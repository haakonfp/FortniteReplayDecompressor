using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Unreal.Core.Extensions;

namespace Unreal.Core.Models
{
    public class NetGuidCache
    {
        public Dictionary<string, NetFieldExportGroup> NetFieldExportGroupMap { get; private set; } = new Dictionary<string, NetFieldExportGroup>();
        public Dictionary<uint, NetFieldExportGroup> NetFieldExportGroupIndexToGroup { get; private set; } = new Dictionary<uint, NetFieldExportGroup>();
        //public Dictionary<uint, NetGuidCacheObject> ImportedNetGuids { get; private set; } = new Dictionary<uint, NetGuidCacheObject>();
        public Dictionary<uint, string> NetGuidToPathName { get; private set; } = new Dictionary<uint, string>();
		public Dictionary<uint, ExternalData> ExternalData { get; private set; } = new Dictionary<uint, ExternalData>();


		public NetFieldExportGroup NetworkGameplayTagNodeIndex { get; private set; }

        private Dictionary<uint, NetFieldExportGroup> _archTypeToExportGroup = new Dictionary<uint, NetFieldExportGroup>();
        public Dictionary<uint, NetFieldExportGroup> NetFieldExportGroupMapPathFixed { get; private set; } = new Dictionary<uint, NetFieldExportGroup>();
        private Dictionary<uint, string> _cleanedPaths = new Dictionary<uint, string>();
        private Dictionary<string, string> _cleanedClassNetCache = new Dictionary<string, string>();
        private Dictionary<string, string> _partialPathNames = new Dictionary<string, string>();
        private HashSet<string> _failedPaths = new HashSet<string>(); //Path names that didn't find an export group

        private Dictionary<string, NetFieldExportGroup> _pathToExportGroup = new Dictionary<string, NetFieldExportGroup>();

        public void ClearCache()
        {
            NetFieldExportGroupMap.Clear();
            NetFieldExportGroupIndexToGroup.Clear();
            NetGuidToPathName.Clear();
            NetFieldExportGroupMapPathFixed.Clear();
			ExternalData.Clear();
			NetworkGameplayTagNodeIndex = null;

            _archTypeToExportGroup.Clear();
            _cleanedClassNetCache.Clear();
            _partialPathNames.Clear();
            _cleanedPaths.Clear();
            _failedPaths.Clear();
            _pathToExportGroup.Clear();
        }

        public void AddToExportGroupMap(string group, NetFieldExportGroup exportGroup)
        {
            if (NetworkGameplayTagNodeIndex == null && group == "NetworkGameplayTagNodeIndex")
            {
                NetworkGameplayTagNodeIndex = exportGroup;
            }

            //Easiest way to do this update
            if(group.EndsWith("ClassNetCache"))
            {
                exportGroup.PathName = Utilities.RemoveAllPathPrefixes(exportGroup.PathName);
            }

            NetFieldExportGroupMap[group] = exportGroup;

            //Check if partial path
            foreach (KeyValuePair<string, string> partialRedirectKvp in CoreRedirects.PartialRedirects)
            {
                if (group.StartsWith(partialRedirectKvp.Key))
                {
                    _partialPathNames.TryAdd(group, partialRedirectKvp.Value);
                    _partialPathNames.TryAdd(Utilities.RemoveAllPathPrefixes(group), partialRedirectKvp.Value);

                    break;
                }
            }
        }

        public NetFieldExportGroup GetNetFieldExportGroup(string pathName)
        {
            if (String.IsNullOrEmpty(pathName))
            {
                return null;
            }

            if (NetFieldExportGroupMap.TryGetValue(pathName, out NetFieldExportGroup netFieldExportGroup))
            {
                return netFieldExportGroup;
            }

            return null;
        }

        public NetFieldExportGroup GetNetFieldExportGroup(uint guid)
        {
            if (!_archTypeToExportGroup.ContainsKey(guid))
            {
                if (!NetGuidToPathName.ContainsKey(guid))
                {
                    return null;
                }

                var path = NetGuidToPathName[guid];

                //Don't need to recheck. Some export groups are added later though
                if (_failedPaths.Contains(path))
                {
                    return null;
                }

                path = CoreRedirects.GetRedirect(path);

                if (_partialPathNames.TryGetValue(path, out string redirectPath))
                {
                    path = redirectPath;
                }

                if (NetFieldExportGroupMapPathFixed.TryGetValue(guid, out var exportGroup) || _pathToExportGroup.TryGetValue(path, out exportGroup))
                {
                    _archTypeToExportGroup[guid] = exportGroup;

                    return exportGroup;
                }

                foreach (var groupPathKvp in NetFieldExportGroupMap)
                {
                    var groupPath = groupPathKvp.Key;

                    if (groupPathKvp.Value.CleanedPath == null)
                    {
                        groupPathKvp.Value.CleanedPath = Utilities.RemoveAllPathPrefixes(groupPath);
                    }

                    if (path.Contains(groupPathKvp.Value.CleanedPath))
                    {
                        NetFieldExportGroupMapPathFixed[guid] = groupPathKvp.Value;
                        _archTypeToExportGroup[guid] = groupPathKvp.Value;
                        _pathToExportGroup[path] = groupPathKvp.Value;

                        return groupPathKvp.Value;
                    }
                }

                //Try fixing ...

                var cleanedPath = Utilities.CleanPathSuffix(path);

                foreach (var groupPathKvp in NetFieldExportGroupMap)
                {
                    if (groupPathKvp.Value.CleanedPath.Contains(cleanedPath))
                    {
                        NetFieldExportGroupMapPathFixed[guid] = groupPathKvp.Value;
                        _archTypeToExportGroup[guid] = groupPathKvp.Value;
                        _pathToExportGroup[path] = groupPathKvp.Value;

                        return groupPathKvp.Value;
                    }
                }

                _failedPaths.Add(path);

                return null;
            }
            else
            {
                return _archTypeToExportGroup[guid];
            }
        }

        public NetFieldExportGroup GetNetFieldExportGroupForClassNetCache(string group, bool fullPath = false)
        {
            if (!_cleanedClassNetCache.TryGetValue(group, out var classNetCachePath))
            {
                if (fullPath)
                {
                    classNetCachePath = $"{group}_ClassNetCache";
                }
                else
                {
                    classNetCachePath = $"{Utilities.RemoveAllPathPrefixes(group)}_ClassNetCache";
                }

                _cleanedClassNetCache[group] = classNetCachePath;
            }

            if (!NetFieldExportGroupMap.ContainsKey(classNetCachePath))
            {
                return default;
            }

            return NetFieldExportGroupMap[classNetCachePath];
        }
    }
}
