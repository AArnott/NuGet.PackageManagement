﻿using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NuGet.PackageManagement.UI
{
    public sealed class SimpleSearchBox : TextBox, IVsWindowSearchHost
    {
        public SimpleSearchBox()
        {

        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public string HelpTopic
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsPopupVisible
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private bool _visible;
        public bool IsVisible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        public void SearchAsync(IVsSearchQuery pSearchQuery)
        {
            throw new NotImplementedException();
        }

        public IVsWindowSearchEvents SearchEvents
        {
            get { throw new NotImplementedException(); }
        }

        public IVsWindowSearch SearchObject
        {
            get { throw new NotImplementedException(); }
        }

        public IVsSearchQuery SearchQuery
        {
            get { return new SimpleSearchQuery(); }
        }

        public IVsSearchQueryParser SearchQueryParser
        {
            get { throw new NotImplementedException(); }
        }

        public IVsSearchTask SearchTask
        {
            get { throw new NotImplementedException(); }
        }

        public void SetupSearch(IVsWindowSearch pWindowSearch)
        {

        }

        public void TerminateSearch()
        {
            throw new NotImplementedException();
        }

        internal class SimpleSearchQuery : IVsSearchQuery
        {
            public uint GetTokens(uint dwMaxTokens, IVsSearchToken[] rgpSearchTokens)
            {
                throw new NotImplementedException();
            }

            public uint ParseError
            {
                get { throw new NotImplementedException(); }
            }

            public string SearchString
            {
                get { return string.Empty; }
            }
        }
    }
}