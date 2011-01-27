using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITJZ.SearchHelper.Client.Util;
using System.Threading;

namespace ITJZ.SearchHelper.Client.Model
{
    [Serializable]
    public class Category
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; }

        public static List<Category> getMenu(bool needUpdate)
        {
            return DataServer.DataServer.getInstance().getCategoryList(Model.User.getCurrentUser(true).Guid, needUpdate);
        }

        public static Category getCategoryByGuid()
        {
            return new Category();
        }

        internal static void save(Category tCategory)
        {
            ApiUrlBuilder builder = new ApiUrlBuilder();
            builder.AddParam("method", "saveCategory");
            builder.AddParam("guid", tCategory.Guid);
            builder.AddParam("name", tCategory.Name);
            string apiUrl = builder.ToString();

            FrmProcess frm = FrmProcess.getInstance();

            frm.Show2(delegate
            {
                String content = Util.Tools.DownloadString(apiUrl);
                var doc = Util.Tools.buildXDoc(content);

                frm.Hide();
                if (ModelUtil.IsSuccess(ref doc))
                {
                    MessageBox.Show(ModelUtil.GetMessage(ref doc));
                }
                else
                {
                    MessageBox.Show(ModelUtil.GetMessage(ref doc));
                }
            });
        }

        internal static void delete(Category tCategory)
        {
            ApiUrlBuilder builder = new ApiUrlBuilder();
            builder.AddParam("method", "deleteCategory");
            builder.AddParam("guid", tCategory.Guid);
            string apiUrl = builder.ToString();

            FrmProcess frm = FrmProcess.getInstance();

            frm.Show2(delegate
            {
                String content = Util.Tools.DownloadString(apiUrl);
                var doc = Util.Tools.buildXDoc(content);

                frm.Hide();
                if (ModelUtil.IsSuccess(ref doc))
                {
                    MessageBox.Show(ModelUtil.GetMessage(ref doc));
                }
                else
                {
                    MessageBox.Show(ModelUtil.GetMessage(ref doc));
                }
            });
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
