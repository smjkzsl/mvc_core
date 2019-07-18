﻿using System.Collections.Generic;

namespace Workman.Util
{
    /// <summary>
    /// 版 本  Workman-Lib V1.0.0CoreMvc
    /// Copyright (c) 2013-2018 水墨剑客
    /// 创建人：水墨剑客
    /// 日 期：2019.04.07
    /// 树结构数据
    /// </summary>
    public static class TreeDataMake
    {
        /// <summary>
        /// 树形数据转化
        /// </summary>
        /// <param name="list">数据</param>
        /// <returns></returns>
        public static List<TreeModel> ToTree(this List<TreeModel> list,string parentId = "")
        {
            Dictionary<string, List<TreeModel>> childrenMap = new Dictionary<string, List<TreeModel>>();
            Dictionary<string, TreeModel> parentMap = new Dictionary<string, TreeModel>();
            List<TreeModel> res = new List<TreeModel>();

            //首先按照
            foreach (var node in list)
            {
                node.hasChildren = false;
                node.complete = true;
                // 注册子节点映射表
                if (!childrenMap.ContainsKey(node.parentId))
                {
                    childrenMap.Add(node.parentId, new List<TreeModel>());
                }
                else if (parentMap.ContainsKey(node.parentId))
                {
                    parentMap[node.parentId].hasChildren = true;
                }
                childrenMap[node.parentId].Add(node);
                // 注册父节点映射节点表
                parentMap.Add(node.id, node);

                // 查找自己的子节点
                if (!childrenMap.ContainsKey(node.id))
                {
                    childrenMap.Add(node.id, new List<TreeModel>());
                }
                else
                {
                    node.hasChildren = true;
                }
                node.ChildNodes = childrenMap[node.id];
            }

            if (string.IsNullOrEmpty(parentId))
            {
                // 获取祖先数据列表
                foreach (var item in childrenMap)
                {
                    if (!parentMap.ContainsKey(item.Key))
                    {
                        res.AddRange(item.Value);
                    }
                }
            }
            else {
                if (childrenMap.ContainsKey(parentId))
                {
                    return childrenMap[parentId];
                }
                else {
                    return new List<TreeModel>();
                }
            }
            return res;
        }

        /// <summary>
        /// 树形数据转化
        /// </summary>
        /// <param name="list">数据</param>
        /// <returns></returns>
        public static List<TreeModelEx<T>> ToTree<T>(this List<TreeModelEx<T>> list) where T : class
        {
            Dictionary<string, List<TreeModelEx<T>>> childrenMap = new Dictionary<string, List<TreeModelEx<T>>>();
            Dictionary<string, TreeModelEx<T>> parentMap = new Dictionary<string, TreeModelEx<T>>();
            List<TreeModelEx<T>> res = new List<TreeModelEx<T>>();

            //首先按照
            foreach (var node in list)
            {
                // 注册子节点映射表
                if (!childrenMap.ContainsKey(node.parentId))
                {
                    childrenMap.Add(node.parentId, new List<TreeModelEx<T>>());
                }
                childrenMap[node.parentId].Add(node);
                // 注册父节点映射节点表
                parentMap.Add(node.id, node);

                // 查找自己的子节点
                if (!childrenMap.ContainsKey(node.id))
                {
                    childrenMap.Add(node.id, new List<TreeModelEx<T>>());
                }
                node.ChildNodes = childrenMap[node.id];
            }
            // 获取祖先数据列表
            foreach (var item in childrenMap)
            {
                if (!parentMap.ContainsKey(item.Key))
                {
                    res.AddRange(item.Value);
                }
            }
            return res;
        }

        /// <summary>
        /// 树形数据转化
        /// </summary>
        /// <param name="list">数据</param>
        /// <returns></returns>
        public static List<TreeMenu> ToMenuTree(this List<TreeMenu> list, string parentId = "")
        {
            Dictionary<string, List<TreeMenu>> childrenMap = new Dictionary<string, List<TreeMenu>>();
            Dictionary<string, TreeMenu> parentMap = new Dictionary<string, TreeMenu>();
            List<TreeMenu> res = new List<TreeMenu>();

            //首先按照
            foreach (var node in list)
            {
                node.hasChildren = false;
                node.complete = true;
                // 注册子节点映射表
                if (!childrenMap.ContainsKey(node.parentId))
                {
                    childrenMap.Add(node.parentId, new List<TreeMenu>());
                }
                else if (parentMap.ContainsKey(node.parentId))
                {
                    parentMap[node.parentId].hasChildren = true;
                }
                childrenMap[node.parentId].Add(node);
                // 注册父节点映射节点表
                parentMap.Add(node.id, node);

                // 查找自己的子节点
                if (!childrenMap.ContainsKey(node.id))
                {
                    childrenMap.Add(node.id, new List<TreeMenu>());
                }
                else
                {
                    node.hasChildren = true;
                }
                node.ChildNodes = childrenMap[node.id];
            }

            if (string.IsNullOrEmpty(parentId))
            {
                // 获取祖先数据列表
                foreach (var item in childrenMap)
                {
                    if (!parentMap.ContainsKey(item.Key))
                    {
                        res.AddRange(item.Value);
                    }
                }
            }
            else
            {
                if (childrenMap.ContainsKey(parentId))
                {
                    return childrenMap[parentId];
                }
                else
                {
                    return new List<TreeMenu>();
                }
            }
            return res;
        }
    }
}
