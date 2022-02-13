using System;
using System.Collections.Generic;
using System.Text;
using CSharpTest.Net.Collections;
using Umbraco.Cms.Infrastructure.PublishedCache.Persistence;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    public class BPlusTreeTransactionScope<TKey, TValue> : ITransactionScope
    {
        private readonly BPlusTree<TKey, TValue> _bplusTree;

        public BPlusTreeTransactionScope(BPlusTree<TKey, TValue> bplusTree)
        {
            _bplusTree = bplusTree;
        }
        public void Commit()
        {
            _bplusTree.Commit();
        }

        public void Dispose()
        {
            //Don't Dispose for BPlusTree
        }

        public void Rollback()
        {
            _bplusTree.Rollback();
        }
    }
}
