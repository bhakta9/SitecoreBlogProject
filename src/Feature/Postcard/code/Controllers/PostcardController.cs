using BlogProject.Feature.Postcard.Models;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Feature.Postcard.Controllers
{
    public class PostcardController : Controller
    {
        // GET: Postcard
        public ActionResult Postcard()
        {

            var result = new List<BlogArticle>();
            try
            {
                Sitecore.Data.Database database = Sitecore.Configuration.Factory.GetDatabase("web");
                Sitecore.Data.Items.Item[] allItems = database.SelectItems(@"fast:/sitecore/content/Home/SitecorePowerBlog/Article//*[@@templateid='{237637D9-7B77-4947-9A28-97C4CC789D56}']");

                foreach (var item in allItems)
                {
                    Sitecore.Data.Fields.ReferenceField dl = item.Fields["Category"];
                    string str = dl != null && dl.TargetItem != null ? dl.TargetItem.Name : "";
                    result.Add(new BlogArticle
                    {
                        Id = FieldRenderer.Render(item, "Id"),
                        Title = FieldRenderer.Render(item, "Title"),
                        ShortDescription = FieldRenderer.Render(item, "ShortDescription"),
                        ShortImage = FieldRenderer.Render(item, "Article Small Image"),
                        PostedDate = FieldRenderer.Render(item, "PostedDate"),
                        Author = FieldRenderer.Render(item, "Author"),
                        Category = str//FieldRenderer.Render(item, "Category"),
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //var items = Sitecore.Context.Database.SelectItems("{56741DF6-7290-4E29-B0D1-11A05EE63AFF}");

            return View("~/Views/SitecorePowerBlog/Postcard.cshtml", result);
        }

        public ActionResult blogdetail(string blogId)
        {
            ViewBag.blogId = blogId;
            var result = new BlogArticle();
            try
            {

                var item = Sitecore.Context.Database.GetItem(blogId);
                Sitecore.Data.Fields.ReferenceField dl = item.Fields["Category"];
                string str = dl != null && dl.TargetItem != null ? dl.TargetItem.Name : "";
                List<string> tags = new List<string>();
                Sitecore.Data.Fields.MultilistField multilistField = item.Fields["Tags"];
                if (multilistField != null)
                {
                    foreach (Sitecore.Data.Items.Item tag in multilistField.GetItems())
                    {
                        tags.Add(tag.Name);
                    }
                }
                result = new BlogArticle
                {
                    Id = FieldRenderer.Render(item, "Id"),
                    Title = FieldRenderer.Render(item, "Title"),
                    LongDescription = FieldRenderer.Render(item, "LargeDescription"),
                    LargeImage = FieldRenderer.Render(item, "Article Large Image"),
                    PostedDate = FieldRenderer.Render(item, "PostedDate"),
                    Author = FieldRenderer.Render(item, "Author"),
                    Category = str,//FieldRenderer.Render(item, "Category"),
                    Tags = tags
                };
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("~/Views/SitecorePowerBlog/blogdetail.cshtml", result);
        }
    }
}