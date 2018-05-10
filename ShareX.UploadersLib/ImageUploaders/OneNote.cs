#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (c) 2007-2018 ShareX Team

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using Newtonsoft.Json;
using ShareX.HelpersLib;
using ShareX.UploadersLib.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ShareX.UploadersLib.ImageUploaders
{
    public class OneNoteImageUploaderService : ImageUploaderService
    {
        public override ImageDestination EnumValue { get; } = ImageDestination.OneNote;

        public override Icon ServiceIcon => Resources.OneNote;

        public override bool CheckConfig(UploadersConfig config)
        {
            return OAuth2Info.CheckOAuth(config.OneNoteOAuth2Info);
        }

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new OneNote(config.OneNoteOAuth2Info);
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpOneNote;
    }

    public sealed class OneNote : ImageUploader, IOAuth2
    {
        private MicrosoftOAuth2 MicrosoftAuth { get; set; }

        public OneNote(OAuth2Info authInfo)
        {
            MicrosoftAuth = new MicrosoftOAuth2(authInfo, this)
            {
                Scope = "Notes.ReadWrite"
            };
        }

        public OAuth2Info AuthInfo => MicrosoftAuth.AuthInfo;

        public bool RefreshAccessToken()
        {
            return MicrosoftAuth.RefreshAccessToken();
        }

        public bool CheckAuthorization()
        {
            return MicrosoftAuth.CheckAuthorization();
        }

        public string GetAuthorizationURL()
        {
            return MicrosoftAuth.GetAuthorizationURL();
        }

        public bool GetAccessToken(string code)
        {
            return MicrosoftAuth.GetAccessToken(code);
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}