﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zovprofil.zovprofil.Controls;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace Zovprofil.zovprofil
{
    public partial class Production : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlMeta metaKeywords = new HtmlMeta();
            metaKeywords.Name = "keywords";
            metaKeywords.Content = "мебельные фасады, рамочные фасады, профиль, мдф, плинтус, мебель";
            Page.Header.Controls.Add(metaKeywords);

            HtmlMeta metaDesc = new HtmlMeta();
            metaDesc.Name = "description";
            metaDesc.Content = "Широкий ассортимент рамочных фасадов из МДФ в древесных декорах. Плинтусы, профили, арки, полки, балюстрады на заказ от фабрики ЗОВ-Профиль. Коллекции корпусной мебели для спальни и гостиной";
            Page.Header.Controls.Add(metaDesc);




            int Type = 0;
            string Category = "";
            string ItemID = "";
            string SubCategory = "";

            URL.Value = Catalog.URL;

            if (Request.Params["type"] != null)
            {
                Type = Convert.ToInt32(Request.Params["type"]);


                string script = $"<script>var pageType = {Type};</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PageTypeScript", script, false);
            }
                

            if (Request.QueryString["cat"] != null)
                Category = Request.QueryString["cat"];

            if (Request.QueryString["subcat"] != null)
                SubCategory = Request.QueryString["subcat"];

            if (Request.QueryString["item"] != null)
                ItemID = Request.QueryString["item"];

            

            if (Category == "")
            {
                if (Type == 0)
                {
                    MainImageDiv.Src = "/Images/fronts_category.jpg";
                    MainImageDiv.Style["display"] = "block";
                    MainDescriptionDiv.Style.Add("display", "block");
                    MainDescriptionDiv.InnerHtml = "Лицо будущей кухни &mdash; это мебельные фасады." +
                        "<br/>На сегодняшний день существует огромное разнообразие материалов, используемых при производстве фасадов для кухни. Фабрика мебельных фасадов &laquo;" +
                        "ОМЦ-Профиль&raquo; использует материалы, сочетающие в себе такие важные для потребителя характеристики, как качество исполнения, разнообразие цветов, " +
                        "эстетичный внешний вид и долговечность. Компания специализируется на выпуске мебельных фасадов из МДФ, облицованных синтетическими пленками от ведущих поставщиков " +
                        "декоративных покрытий (полипропилен, нанофлекс, ПВХ, финиш-плёнка). Многие коллекции могут быть так же выполнены с нанесением лакокрасочных материалов." +
                        "<br/>На сайте представлены различные виды кухонных фасадов: рамочные и фрезерованные, витрины и глухие с различными видами вставок. Так же к каждой коллекции " +
                        "предлагаются декоративные элементы, которые придадут Вашему интерьеру индивидуальность и законченный вид.";
                }

                if (Type == 1)
                {
                    MainImageDiv.Src = "/Images/fronts_category.jpg";
                    MainImageDiv.Style["display"] = "block";
                    MainDescriptionDiv.Style.Add("display", "block");
                    MainDescriptionDiv.InnerHtml = "ДЕКОРАТИВНЫЕ ЭЛЕМЕНТЫ придают кухне особенный индивидуальный вид. Компания предлагает широкий ассортимент элементов декора для заполнения фасада кухни и обогащения композиционной составляющей кухни: багеты, пилястры, полочницы и бутылочницы, арки, баллюстрады, декоративные накладки и плинтусы." +
"<br /><br/>Все декоративные элементы из МДФ соответствуют цвету, материалу и фактуре предлагаемых кухонных фасадов." +
"<br /><br/>ПОГОНАЖНЫЕ ИЗДЕЛИЯ. ООО «ОМЦ - Профиль» производит мебельный профиль МДФ различной конфигурации, применяемый в производстве корпусной и кухонной мебели: погонажный и наборный профиль, карнизы, накладки, вставки для фасадов, профильные планки." +
"<br /><br/>Облицовка: меламиновая бумага, плёнка ПП и ПВХ от ведущих поставщиков.";
                }

                if (Type == 2)
                {
                    MainImageDiv.Src = "/Images/fronts_category.jpg";
                    MainImageDiv.Style["display"] = "block";
                    MainDescriptionDiv.Style.Add("display", "block");
                    MainDescriptionDiv.InnerHtml = "Выбирая корпусную мебель для гостиной или спальни, вы стараетесь придерживаться определенной стилистики.В нашем каталоге мебели можно выбрать как классические модели гостиных и стенок, так и ультра современные комплекты. Наши производственные возможности удивят вас широтой выбора и качеством продукции.";
                }

                if (Type == 4)
                {
                    MainImageDiv.Src = "/Images/fronts_category.jpg";
                    MainImageDiv.Style["display"] = "block";
                    MainDescriptionDiv.Style.Add("display", "block");
                    MainDescriptionDiv.InnerHtml = "Представить широкий ассортимент нашей продукции в Вашем магазине или выставочном салоне помогут наши рекламные материалы. Среди всего разнообразия наших стендов, стоек, экспозиторов – Вы обязательно найдете то, что подойдёт именно Вашей торговой точке. На них Вы сможете разместить образцы наших фасадов декорэлементов и профилей. Не хватает места в салоне – не беда, специально для Вас мы разработали образцы с минимальными размерами в виде четвертинок фасадов и небольших планшетов с декроэлементами и профилями. Для работы дизайнеров на выезде у клиента мы предлагаем большой ассортимент образцов, которые могут быть укомплектованы удобными экспобоксами, которые облегчат работу с образцами и увеличат срок их службы. Классические рекламные продукты в виде каталогов и вееров облицовочного материала мы тоже рады Вам предоставить.";
                }
                if (Type == 5)
                {
                    MainImageDiv.Src = "/Images/fronts_category.jpg";
                    MainImageDiv.Style["display"] = "block";
                    MainDescriptionDiv.Style.Add("display", "block");
                    MainDescriptionDiv.InnerHtml = "Декоративные рейки в интерьере - современный прием для отделки стен и зонирования. В дизайне помещения рейки могут быть не только акцентным украшением, но и функциональным элементом.<br/>Как можно применить эту конструкцию для вашего интерьера?<br/><br/>1. Зонирование. В больших комнатах с помощью баффелей можно отгородить различные зоны: обеденную и “диванную” – в кухне-гостиной, работы и отдыха – в совмещенной с домашним офисом спальне и так далее. Особенно это решение пригодится в компактных квартирах-студиях: с его помощью можно обозначить в общем пространстве жилища уединенную зону спальни, при этом не лишив ее естественного освещения.<br/><br/>2. Перила. Рейки - отличная альтернатива классическим перилам в домах с лестницей: вертикальные панели на всю высоту пролета так же безопасны, но выглядят более стильно, современно и необычно. Такая лестница непременно станет центром всеобщего внимания и главным украшением вашего дома.<br/><br/>3. Двери. Декоративные планки могут заменить двери шкафов, гардеробных и систем хранения. Такой прием сейчас особенно популярен в лаконичных интерьерах современного стиля и минимализма.<br/><br/>4. Декор. С помощью ламелей можно оформить стены, придав им оригинальную фактуру и объем. Варианты в тон стены добавят глубины помещению в стиле минимализм, контрастные скорректируют геометрию комнаты, модели из дерева украсят интерьеры в экостиле, а металлические – в стиле лофт.<br/><br/>5. Эффектное освещение. При помощи реек можно сделать необычное освещение в интерьерах современного стиля, лофт и хай-тек, оформив лампы как элемент конструкции. Вытянутые светильники замаскировать среди планок. Также можно спрятать светодиодные ленты за баффелями, создав эффект “света изнутри”. Если при этом использовать разноцветную подсветку, она станет ярким элементом ваших вечеринок.";
                }
            }

            DataTable CategoryDT = Catalog.FillCategories(Type);

            foreach (DataRow Row in CategoryDT.Rows)
            {
                LeftMenuItem Item = (LeftMenuItem)Page.LoadControl("~/zovprofil/Controls/LeftMenuItem.ascx");
                Item.Name = Row["Category"].ToString().Replace("РП-", "");
                string encodedCategory = Uri.EscapeDataString(Row["Category"].ToString());
                Item.URL = $"/Production?type={Type}&cat={encodedCategory}";

                if (Category.Length > 0)
                    if (Item.Name == Category)
                        Item.Selected = true;

                if (Type == 0)
                {
                    FrontsContainer.Controls.Add(Item);

                }
                if (Type == 1)
                {
                    DecorContainer.Controls.Add(Item);
                }
                if (Type == 2)
                {
                    CabsContainer.Controls.Add(Item);
                    
                }
                if (Type == 3)
                    ReadyContainer.Controls.Add(Item);
                if(Type == 4)
                {
                    PromotionContainer.Controls.Add(Item);
                }
                if (Type == 5)
                {
                    InteriorContainer.Controls.Add(Item);
                }
            }
            HtmlGenericControl hr = new HtmlGenericControl("hr");
            hr.Style["width"] = "100%";
            hr.Style["color"] = "#fff";

            HtmlGenericControl link = new HtmlGenericControl("a");
            link.Attributes["href"] = "https://exclusive-zov.wellmaker.by/Production";
            link.InnerHtml = "Эксклюзив ZOV";
            link.Attributes["class"] = "lmenu-item";
            link.Style["text-align"] = "left";

            FrontsContainer.Controls.Add(hr);
            FrontsContainer.Controls.Add(link);





            if (Category.Length == 0)
            {
                foreach (DataRow Row in CategoryDT.Rows)
                {
                    ProductMenuItem Item = (ProductMenuItem)Page.LoadControl("~/zovprofil/Controls/ProductMenuItem.ascx");
                    Item.ProductCategory = Row["Category"].ToString();

                    Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();
                    string encodedCategory = Uri.EscapeDataString(Row["Category"].ToString());
                    Item.URL = $"/Production?type={Type}&cat={encodedCategory}";

                    ProductMenu.Controls.Add(Item);
                }
            }
            else if (Type == 1 && Category.Length > 0 && SubCategory.Length == 0)
            {
                DataTable BasicDecorsDT = Catalog.FillBasicDecors(Category);
                foreach (DataRow Row in BasicDecorsDT.Rows)
                {
                    ProductMenuItem Item = (ProductMenuItem)Page.LoadControl("~/zovprofil/Controls/ProductMenuItem.ascx");


                    string name = Row["Name"].ToString();
                    int spaceIndex = name.IndexOf(' ');
                    Item.ProductCategory = spaceIndex >= 0 ? name.Substring(0, spaceIndex) : name;



                    //Item.ProductCategory = Row["Name"].ToString().Substring(0, Row["Name"].ToString().IndexOf(' '));

                    Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();
                    string encodedCategory = Uri.EscapeDataString(Category);
                    Item.URL = $"/Production?type={Type}&cat={encodedCategory}&subcat={Item.ProductCategory}";

                    ProductMenu.Controls.Add(Item);
                }
            }
            else if (ItemID.Length == 0)
            {
                MainDescriptionDiv.Style.Add("display", "none");

                DataTable ProductsDT;
                if (Type == 1)
                    ProductsDT = Catalog.FillNotBasicDecors(SubCategory, Category);
                else
                    ProductsDT = Catalog.FillProducts(Type, Category);

                foreach (DataRow Row in ProductsDT.Rows)
                {
                    bool existFlag = Catalog.CheckFileExists("/zovprofil.by/wwwroot/Images/ClientsCatalogImages/Thumbs/" + Row["FileName"].ToString());
                    if (!existFlag)
                    {
                        Catalog.ProcessProductImage("/zovprofil.by/wwwroot/Images/ClientsCatalogImages/" + Row["FileName"].ToString(), "/zovprofil.by/wwwroot/Images/ClientsCatalogImages/Thumbs/" + Row["FileName"].ToString());
                    }

                    ProductItem Item = (ProductItem)Page.LoadControl("~/zovprofil/Controls/ProductItem.ascx");
                    Item.Name = Row["Name"].ToString() + "<br/>" + Row["Color"].ToString();
                    Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();
                    string encodedCategory = Uri.EscapeDataString(Category);

                    if(Type == 1)
                        Item.URL = $"/Production?type={Type}&cat={encodedCategory}&subcat={SubCategory}&item={Row["ImageID"]}";
                    else
                        Item.URL = $"/Production?type={Type}&cat={encodedCategory}&item={Row["ImageID"]}";

                    ProductMenu.Controls.Add(Item);
                }
            }





            //if (Category.Length > 0 && ItemID.Length == 0)
            //{
            //    MainDescriptionDiv.Style.Add("display", "none");

            //    DataTable ProductsDT = Catalog.FillProducts(Type, Category);

            //    foreach (DataRow Row in ProductsDT.Rows)
            //    {
            //        bool existFlag = Catalog.CheckFileExists("/zovprofil.by/wwwroot/Images/ClientsCatalogImages/Thumbs/" + Row["FileName"].ToString());
            //        if (!existFlag)
            //        {
            //            Catalog.ProcessProductImage("/zovprofil.by/wwwroot/Images/ClientsCatalogImages/" + Row["FileName"].ToString(), "/zovprofil.by/wwwroot/Images/ClientsCatalogImages/Thumbs/" + Row["FileName"].ToString());
            //        }


            //        ProductItem Item = (ProductItem)Page.LoadControl("~/zovprofil/Controls/ProductItem.ascx");
            //        Item.Name = Row["Name"].ToString().Replace("РП-", "") + "<br/>" + Row["Color"].ToString();
            //        Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();

            //        string encodedCategory = Uri.EscapeDataString(Category);
            //        Item.URL = $"/Production?type={Type}&cat={encodedCategory}&item={Row["ImageID"]}";






            //        if(Type == 1)
            //        {
            //            string temp = Row["Name"].ToString();
            //            Item.Name = temp.Substring(0, temp.IndexOf(' '));

            //            Item.URL = $"/Production?type={Type}&cat={encodedCategory}&subcat={Item.Name}";
            //        }
            //        ProductMenu.Controls.Add(Item);
            //    }
            //}
            ////else if (SubCategory.Length == 0)
            ////{

            ////}
            //else
            //{
            //    foreach (DataRow Row in CategoryDT.Rows)
            //    {
            //        ProductMenuItem Item = (ProductMenuItem)Page.LoadControl("~/zovprofil/Controls/ProductMenuItem.ascx");
            //        Item.ProductCategory = Row["Category"].ToString();
     
            //        //if (Type == 2)
            //        //{
            //        //    if (Item.ProductCategory.ToLower() == "куб")
            //        //    {
            //        //        Item.ProductImageUrl = "https://zovprofil.by/Images/КУБ.jpeg";
            //        //    }
            //        //    else if (Item.ProductCategory.ToLower() == "норманн")
            //        //    {
            //        //        Item.ProductImageUrl = "https://zovprofil.by/Images/НОРМАНН.jpeg";
            //        //    }
            //        //    else if (Item.ProductCategory.ToLower() == "патриция")
            //        //    {
            //        //        Item.ProductImageUrl = "https://zovprofil.by/Images/патриция.jpeg";
            //        //    }
            //        //    else if (Item.ProductCategory.ToLower() == "мягкая")
            //        //    {
            //        //        Item.ProductImageUrl = "https://zovprofil.by/Images/мягкая.jpeg";
            //        //    }
            //        //    else
            //        //        Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();
            //        //}
            //        //if(Type == 5)
            //        //{
            //        //    if(Item.ProductCategory.ToLower() == "панель наборная")
            //        //    {
            //        //        Item.ProductImageUrl = "https://zovprofil.by/Images/мягкая.jpeg";
            //        //    }
            //        //    else
            //        //        Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();
            //        //}
            //        //else
            //            Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();

            //        string encodedCategory = Uri.EscapeDataString(Row["Category"].ToString());
            //        Item.URL = $"/Production?type={Type}&cat={encodedCategory}";

            //        ProductMenu.Controls.Add(Item);
            //    }
            //}

            if (ItemID.Length > 0)
            {
                CreateCatSlider(Category, true);


                ProductMenu.Visible = false;
                ProductItemCont.Style.Add("display", "flex");

                string sFileName = "";
                string sName = "";
                string sDescription = "";
                string sMaterial = "";
                string sSizes = "";
                string sConfigID = "";
                string sTechID = "";
                string sProductType = "";
                string sColor = "";
                string sBasic = "";
                string sCategory = "";
                string sPatinaID = "";
                string sColorID = "";

                Catalog.GetItemDetail(Convert.ToInt32(ItemID), ref sFileName, ref sName, ref sDescription, ref sMaterial, ref sSizes, ref sConfigID, ref sProductType, ref sTechID, ref sColor, ref sBasic, ref sCategory, ref sPatinaID, ref sColorID);

                if (sDescription.Length == 0)
                    DescriptionDiv.Visible = false;
                if (sMaterial.Length == 0)
                    MaterialDiv.Visible = false;
                if (sSizes.Length == 0)
                    SizesDiv.Visible = false;


                
                string sTechStoreFile = "";
                try
                {
                    sTechStoreFile = Catalog.GetTechStoreImage(Convert.ToInt32(sTechID));

                    string t = sFileName;
                    bool existFlag = Catalog.CheckFileExists("/zovprofil.by/wwwroot/Images/ClientsCatalogImages/Thumbs/" + sFileName);
                    if (!existFlag)
                    {
                        Catalog.ProcessProductImage("/zovprofil.by/wwwroot/Images/ClientsCatalogImages/" + sFileName, "/zovprofil.by/wwwroot/Images/ClientsCatalogImages/Thumbs/" + sFileName);
                    }
                }
                catch
                {

                }
                //FillProductSlider(sFileName, sTechStoreFile, bool.Parse(sBasic), Type);
                FillProductImages(sFileName, sTechStoreFile, sBasic, Type);




                /*ProductItemImage.Src = Catalog.URL + "Thumbs/" + sFileName;*/

                //ProductItemName.InnerHtml = sName;
                //if(Type == 1 || Type == 0)
                //    ProductItemName.InnerHtml = "<b>" + SplitName(Category, sName, sColor) + "</b>";
                //else
                    ProductItemName.InnerHtml = "<b>" + sName + "<br>" + sColor + "</b>";


                Description.InnerHtml = sDescription.Replace("\n", "<br />");
                Material.InnerHtml = sMaterial.Replace("\n", "<br />");
                Sizes.InnerHtml = sSizes.Replace("\n", "<br />");


                int sMatrixID = 0;
                Catalog.GetMatrixIdFromConfID(Convert.ToInt32(sConfigID), ref sMatrixID);


                if (Type == 0)
                {
                    ColorDiv.Style["display"] = "block";
                    int test1 = sMatrixID;
                    string test2 = GetBasicCategory(sMatrixID);
                    DataRow test3;
                    string test4 = "";
                    foreach (DataRow row in GetCategoryColors(GetBasicCategory(sMatrixID)).Rows)
                    {
                        test3 = row;
                        test4 += CreateCategoryColorsLinks(row);
                    }


                    foreach (DataRow row in GetCategoryColors(GetBasicCategory(sMatrixID)).Rows)
                    {
                        Colors.InnerHtml += CreateCategoryColorsLinks(row);
                    }
                }
                
                




                

                DataTable ProductsDT = Catalog.FillRelatedDecors(sMatrixID);


                bool haveDecors = false;
                foreach (DataRow Row in ProductsDT.Rows)
                {
                    ProductItem Item = (ProductItem)Page.LoadControl("~/zovprofil/Controls/ProductItem.ascx");

                    Item.Name = Row["Name"].ToString() + "</br>" + Row["Color"].ToString();
                    Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();
                    Item.URL = "/Production?type=" + 1 + "&cat=" + Row["Category"] + "&item=" + Row["ImageID"].ToString();

                    RelatedDecors.Controls.Add(Item);

                    haveDecors = true;
                }

                if (haveDecors && Type == 0)
                    RelatedDecorsDiv.Style["display"] = "flex";

                DataTable NotBasicDT = Catalog.FillNotBasicFronts(sMatrixID);

                if (sProductType == "0" && NotBasicDT.Rows.Count > 1)
                    NotBasicFrontsDiv.Style["display"] = "flex";


                //чтобы текущий фасад был последним
                ProductItem temp_item = (ProductItem)Page.LoadControl("~/zovprofil/Controls/ProductItem.ascx");
                bool flag = false;
                foreach (DataRow Row in NotBasicDT.Rows)
                {
                    
                    ProductItem Item = (ProductItem)Page.LoadControl("~/zovprofil/Controls/ProductItem.ascx");

                    string nCategory = Row["Category"].ToString().Replace("Эксклюзив ZOV: ", "");
                    nCategory = nCategory.Replace("ЭП ", "").ToLower();
                    nCategory = nCategory.Replace(" ", "").ToLower();
                    nCategory = char.ToUpper(nCategory[0]) + nCategory.Substring(1);

                    Item.Name = Row["Name"].ToString() + "</br>" + sColor.ToString();
                    Item.ProductImageUrl = Catalog.URL + "Thumbs/" + Row["FileName"].ToString();
                    string encodedCategory = Uri.EscapeDataString(Row["Category"].ToString());
                    Item.URL = $"/Production?type={Type}&cat={encodedCategory}&item={Row["ImageID"]}";


                    if (Row["ImageID"].ToString() == ItemID)
                    {
                        var productText = Item.FindControl("ProdCategory") as HtmlGenericControl;
                        productText.Style["color"] = "#A3C2B9";
                        temp_item = Item;
                        flag = true;
                        continue;
                    }
                    NotBasicFronts.Controls.Add(Item);
                }
                if (flag)
                    NotBasicFronts.Controls.Add(temp_item);


            }
            else
            {

                CreateCatSlider(Category);






                //string sliderNames = "";
                //string sliderUrls = "";

                //if (MainSliderDT.Rows.Count == 0)
                //{
                //    SliderCont.Visible = false;
                //}
                //else
                //{
                //    hDesc.Value = MainSliderDT.Rows[0]["Description"].ToString().Replace("\n", "<br />");

                //    for (int i = 0; i < MainSliderDT.Rows.Count; i++)
                //    {
                //        System.Web.UI.HtmlControls.HtmlGenericControl img = new System.Web.UI.HtmlControls.HtmlGenericControl("img");
                //        img.Attributes.Add("class", "im");
                //        img.ID = "im" + (i + 1).ToString();
                //        img.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                //        img.Attributes.Add("onclick", "OpenSliderImage()");
                //        img.Attributes.Add("src", Catalog.URL + MainSliderDT.Rows[i]["FileName"].ToString());
                //        if (i == 0)
                //        {
                //            img.Style.Add("opacity", "1");
                //        }

                //        System.Web.UI.HtmlControls.HtmlGenericControl pdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                //        if (i == 0)
                //        {
                //            pdiv.Attributes.Add("class", "sl-p");
                //        }

                //        pdiv.ID = "slp" + (i + 1).ToString();
                //        pdiv.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                //        System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                //        div.Attributes.Add("class", "sl");
                //        div.ID = "sl" + (i + 1).ToString();
                //        div.Attributes.Add("onclick", "SelectImage(" + (i + 1) + ")");
                //        div.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                //        div.Controls.Add(pdiv);

                //        SliderNavCont.Controls.Add(div);

                //        ImagesSliderCont.Controls.Add(img);

                //        sliderNames += MainSliderDT.Rows[i]["Name"].ToString() + "123;";
                //        sliderUrls += MainSliderDT.Rows[i]["FileName"].ToString() + ";";
                //    }

                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetSliderNames", "SetSliderNames('" + sliderNames + "');", true);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetSliderUrls", "SetSliderUrls('" + sliderUrls + "');", true);

                //    hSlidesCount.Value = MainSliderDT.Rows.Count.ToString();
                //}
            }
        }

        private void AddSlideToSlider(string imageName, int i, bool swiper1=false)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl img = new System.Web.UI.HtmlControls.HtmlGenericControl("img");
            //img.ID = "im" + (i + 1).ToString();
            img.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            img.Attributes.Add("onclick", "OpenSliderImage()");
            img.Attributes.Add("width", "100%");
            img.Attributes.Add("src", Catalog.URL + imageName);

            System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            div.Attributes.Add("class", "swiper-slide");
            div.ID = "slide" + (i + 1).ToString();
            div.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            div.Controls.Add(img);

            if(swiper1)
                swiperWrapper1.Controls.Add(div);
            else
                swiperWrapper2.Controls.Add(div);
        }

        private void CreateCatSlider(string Category, bool swiper1=false)
        {
            DataTable MainSliderDT = Catalog.FillCatSliderImages(Category);
            string desc = GetCategryDescription(Category);


            if (MainSliderDT.Rows.Count == 0)
            {
                if (swiper1)
                {
                    Swiper1.Visible = false;
                    DescriptionText1.Visible = false;
                }
                    
                else
                {
                    Swiper2.Visible = false;
                    DescriptionText2.Visible = false;
                }
                    
            }
            else
            {
                for (int i = 0; i < MainSliderDT.Rows.Count; i++)
                {
                    string imageName = MainSliderDT.Rows[i]["FileName"].ToString();
                    if (swiper1) {
                        AddSlideToSlider(imageName, i, true);
                        DescriptionText1.InnerHtml = desc + "<br>";
                        Swiper1.Attributes.CssStyle.Add("display", "block");
                    }
                        
                    else
                    {
                        AddSlideToSlider(imageName, i);
                        DescriptionText2.InnerHtml = desc + "<br>";
                        Swiper2.Attributes.CssStyle.Add("display", "block");
                    }
                        
                }
            }

            

        }

        private string GetCategryDescription(string Category)
        {
            string description = string.Empty;

            using (SqlConnection connection = new SqlConnection(Catalog.ConnectionString))
            {
                string query = "SELECT Description FROM [infiniu2_catalog].[dbo].[ClientsCatalogImages] WHERE Category = @Category AND ProductType = 3 AND Description <> '' AND Description IS NOT NULL";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Category", Category);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        description = reader["Description"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return description;
        }


        private string SplitName(string Category, string Name, string Color)
        {
            // Убираем материал из названия категории
            Category = Category.Replace(" ПР3", "");
            Category = Category.Replace(" RAL", "");

            //Определяем последнее слово категории(главное)
            string[] cat = Category.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string category = cat[cat.Length - 1];

            string pattern = @"(.*?)(?i:" + Regex.Escape(category) + @")(\s*)(.*)";

            Match match = Regex.Match(Name, pattern);

            if (match.Success)
            {
                //До категории
                string beforeLastWord = match.Groups[1].Value.Trim();
                //После
                string afterLastWord = match.Groups[3].Value.Trim();

                string formattedFacadeName = beforeLastWord + " " + category + (afterLastWord != "" ? "<br>" : "") + afterLastWord;

                return formattedFacadeName + "<br>" + Color;
            }
            return null;
        }


        private string GetBasicCategory(int sMatrixID)
        {
            string Select = @"SELECT [Category]
                        FROM CollectionsConfig 
                        INNER JOIN FrontsConfig 
                            ON CollectionsConfig.ConfigId2 = FrontsConfig.MatrixID 
                        INNER JOIN ClientsCatalogFrontsConfig 
                            ON FrontsConfig.FrontID = ClientsCatalogFrontsConfig.FrontID 
                                AND ClientsCatalogFrontsConfig.ColorID = FrontsConfig.ColorID 
                                AND ClientsCatalogFrontsConfig.PatinaID = FrontsConfig.PatinaID 
                                AND ClientsCatalogFrontsConfig.InsetColorID = FrontsConfig.InsetColorID 
                                AND ClientsCatalogFrontsConfig.InsetTypeID = FrontsConfig.InsetTypeID 
                        INNER JOIN ClientsCatalogImages 
                            ON ClientsCatalogFrontsConfig.ConfigID = ClientsCatalogImages.ConfigID 
                        WHERE CollectionsConfig.ConfigId1 = @sMatrixID AND ProductType = 0 AND ToSite = 1 AND Category NOT LIKE '%Эксклюзив%' AND Basic = 1";


            using (SqlConnection conn = new SqlConnection(Catalog.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Select, conn);
                cmd.Parameters.Add("@sMatrixID", SqlDbType.Int);
                cmd.Parameters["@sMatrixID"].Value = sMatrixID;

                try
                {
                    conn.Open();
                    return cmd.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    return "";
                }
            }



            
        }

        private DataTable GetCategoryColors(string category)
        {
            string query = @"WITH CTE AS (
                                SELECT
                                    ImageID,
                                    FileName,
                                    Category,
                                    Color,
                                    Basic,
                                    ROW_NUMBER() OVER (PARTITION BY Category, Color ORDER BY Basic DESC) AS rn
                                FROM [infiniu2_catalog].[dbo].[ClientsCatalogImages]
                                WHERE Category = @Category AND Color <> '' AND Color IS NOT NULL AND ToSite = 1 AND Basic = 1
                            )
                            SELECT ImageID, FileName, Category, Color, Basic
                            FROM CTE
                            WHERE rn = 1
                            ORDER BY Color ASC";

            using (SqlConnection connection = new SqlConnection(Catalog.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Category", category);

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }


        private DataTable GetColorsByName(string name, string type)
        {
            string query = @"WITH CTE AS (
                                SELECT
                                    ImageID,
                                    FileName,
                                    Category,
                                    Color,
                                    Basic,
                                    ROW_NUMBER() OVER (PARTITION BY Category, Color ORDER BY Basic DESC) AS rn
                                FROM [infiniu2_catalog].[dbo].[ClientsCatalogImages]
                                WHERE Category = @Category AND Color <> '' AND Color IS NOT NULL AND ToSite = 1 AND Basic = 1
                            )
                            SELECT ImageID, FileName, Category, Color, Basic
                            FROM CTE
                            WHERE rn = 1
                            ORDER BY Color ASC";

            using (SqlConnection connection = new SqlConnection(Catalog.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Category", name);

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        private string CreateCategoryColorsLinks(DataRow row)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl img = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            img.Attributes.Add("class", "product-color-image");
            img.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            string imageUrl = "https://zovprofil.by/Images/ClientsCatalogImages/Thumbs/" + row["FileName"];
            img.Attributes.CssStyle.Add("background-image", $"url('{imageUrl}')");
            img.Attributes.Add("title", (string)row["Color"]);

            System.Web.UI.HtmlControls.HtmlGenericControl img_wrapper = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            img_wrapper.Attributes.Add("class", "product-color-img_wrapper");
            img_wrapper.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            img_wrapper.Controls.Add(img);

            System.Web.UI.HtmlControls.HtmlGenericControl a = new System.Web.UI.HtmlControls.HtmlGenericControl("a");
            a.Attributes.Add("class", "product-color-block");
            a.Attributes.Add("href", "/Production?type=0&cat=" + row["Category"].ToString() + "&item=" + row["ImageID"].ToString());
            a.Controls.Add(img_wrapper);

            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                {
                    a.RenderControl(writer);
                    return stringWriter.ToString();
                }
            }
        }

        private void FillProductImages(string ProductFile, string TechStoreFile, string Basic, int Type)
        {
            product_image_main.Style["display"] = "block";
            product_image_main.Attributes.Add("src", Catalog.URL + "Thumbs/" + ProductFile);


            if (TechStoreFile == "" || TechStoreFile == null)
                return;

            if (TechStoreFile.Contains(".tif"))
            {
                TechStoreFile = TiffToPng(TechStoreFile);
            }

            if (Type == 0 && Basic == "False")
            {
                return;
            }
            else
            {
                product_image_tech.Style["display"] = "block";
                product_image_tech.Attributes.Add("src", "/Images/TechStore/" + TechStoreFile);
            }
        }

        private string TiffToPng(string FileName)
        {
            try
            {
                string NewFileName = FileName.Replace(".tif", ".png");
                //string test = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/TechStore/");
                Bitmap.FromFile(path + FileName).Save(path + NewFileName, System.Drawing.Imaging.ImageFormat.Png);
                return NewFileName;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return "";
        }
    }
}