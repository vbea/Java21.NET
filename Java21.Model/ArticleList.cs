using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbes.WebControls.Mvc;

namespace Java21.Model
{
    public class ArticleList
    {
        public PagedList<Article> articles { get; set; }
        public List<Category> categorys { get; set; }
        public int category { get; set; }
        public void dataBind()
        {
            foreach (Article art in articles)
            {
                art.catename = getCategory(art.category);
            }
        }

        private string getCategory(int id)
        {
            foreach (Category cate in categorys)
            {
                if (cate.id == id)
                    return cate.catename;
            }
            return "--";
        }
    }
}
