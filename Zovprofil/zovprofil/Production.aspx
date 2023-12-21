﻿<%@ Page Title="Каталог продукции – фабрика ЗОВ-Профиль" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" ClientIDMode="Static" EnableViewState="false" CodeBehind="Production.aspx.cs" Inherits="Zovprofil.zovprofil.Production" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <link href="../Styles/production.css" rel="stylesheet" />
    <%--<link href="../Styles/bootstrap.css" rel="stylesheet" />--%>
    <script src="../Scripts/jquery-3.1.0.min.js"></script>
    <%--<script src="../Scripts/bootstrap.min.js"></script>--%>
    
    <input type="hidden" id="URL" runat="server" />
    <input type="hidden" runat="server" id="hSlidesCount" />
    <input type="hidden" runat="server" id="hDesc" />

    <div style="text-align:center" class="main-container">
        <%--<div style="/*width:1000px;*/ display:inline-block; margin-top:30px; margin-bottom:30px;">--%>



            <%--<div style="display:inline-block; float:left; /* width:300px*/; border: 1px solid #f5f5f5;">
                <div style="text-align:left; background-color:white; padding-left:15px; padding-top:3px; padding-bottom:5px;">
                    <div>
                        <a href="/Production?type=0">
                            <img src="/Images/fronts.png" style="float:left; width:20px; margin-top:14px;" />
                            <div id="FrontsCat" runat="server" class="lmenu-cat">
                                ФАСАДЫ
                            </div>
                            <img src="/Images/dn.png" id="ExpImageFronts" runat="server" style="margin-top: 21px; float: right; margin-right: 15px;" />
                        </a>
                        <div id="FrontsContainer" runat="server" class="lmenu-cat-cont"></div>


                        <a href="/Production?type=2">
                            <img src="/Images/cup.png" style="float:left; width:20px; margin-top:14px;" />
                            <div id="CabCat" runat="server" class="lmenu-cat">
                                МЕБЕЛЬ
                            </div>
                            <img src="/Images/dn.png" id="ExpImageCup" runat="server" style="margin-top: 21px; float: right; margin-right: 15px;" />
                        </a>
                        <div id="CabsContainer" runat="server" class="lmenu-cat-cont"></div>


                        <a href="/Production?type=1">
                            <img src="/Images/profile.png" style="float:left; width:20px; margin-top:14px;" />
                            <div id="DecorCat" runat="server"  class="lmenu-cat">
                                ДЕКОР И ПОГОНАЖ
                            </div>
                            <img src="/Images/dn.png" id="ExpImageProfile" runat="server" style="margin-top: 21px; float: right; margin-right: 15px;" />
                        </a>
                        <div id="DecorContainer" runat="server" class="lmenu-cat-cont"></div>


                        <a href="/Production?type=4">
                            <img src="/Images/promotion.png" style="float:left; width:20px; margin-top:14px;" />
                            <div id="PromCat" runat="server" class="lmenu-cat">
                                РЕКЛАМНАЯ ПРОДУКЦИЯ
                            </div>
                            <img src="/Images/dn.png" id="ExpImagePromotion" runat="server" style="margin-top: 21px; float: right; margin-right: 15px;" />
                        </a>
                        <div id="PromotionContainer" runat="server" class="lmenu-cat-cont"></div>

                        <a href="/Production?type=5">
                            <img src="/Images/interior.png" style="float:left; width:20px; margin-top:14px;" />
                            <div id="InteriorCat" runat="server" class="lmenu-cat">
                                ИНТЕРЬЕРНЫЕ ДЕКОРЫ
                            </div>
                            <img src="/Images/dn.png" id="ExpImageInterior" runat="server" style="margin-top: 21px; float: right; margin-right: 15px;" />
                        </a>
                        <div id="InteriorContainer" runat="server" class="lmenu-cat-cont"></div>


                        <div id="ReadyContainer" runat="server" class="lmenu-cat-cont"></div>
                    </div>
                </div>
                <div style="text-align:left; background-color:white; height:48px; margin-top:15px; padding-left:15px; padding-top:3px; padding-bottom:5px;">
                   <a href="/Downloads">
                        <img src="/Images/download.png" style="float:left; width:20px; margin-top:14px;" />
                        <div class="lmenu-cat">
                            Скачать каталоги
                        </div>
                        <img src="/Images/right.png" style="margin-top: 21px; float: right; margin-right: 15px;" />
                    </a>
                </div>
                <a href="http://www.wm-shop.by/" onclick="ymetr()" target="_blank" style="margin-right:-4px;">
                    <div class="shopc noselect">
                        <img src="/Images/cart.png" class="img" />
                        <span class="but">ИНТЕРНЕТ-МАГАЗИН</span>
                    </div>
                </a>
            </div>--%>







            <nav>
			    <div id="Sidebar" class="sidebar">
			      <div class="sidebar-content">
			        <article class="category-container">
                        
			    	        <div class="category" onclick="toggleCategory('FrontsContainer', 0, event)">
                                <a id="FrontsLink" href="/Production?type=0">
			    		            <img class="category-img" src="/Images/fronts.png" alt="">
				                    <span>ФАСАДЫ</span>
				                    <span class="arrow"></span>
                                </a>
				            </div>
                        
				        <div id="FrontsContainer"  runat="server" class="category-content">
				          <!-- Список категорий фасадов -->
				        </div>
			        </article>

			        <article class="category-container">
                        
			    	        <div class="category" onclick="toggleCategory('CabsContainer', 2, event)">
                                <a id="CabsLink" href="/Production?type=2">
			    		            <img class="category-img" src="/Images/cup.png" alt="">
				                  <span>МЕБЕЛЬ</span>
				                  <span class="arrow"></span>
                                </a>
				            </div>
                        
				        <div id="CabsContainer" runat="server" class="category-content">
				          <!-- Список категорий мебели -->
				        </div>
			        </article>

			        <article class="category-container">
			    	    <div class="category" onclick="toggleCategory('DecorContainer', 1, event)">
                            <a id="DecorLink" href="/Production?type=1">
			    		        <img class="category-img" src="/Images/profile.png" alt="">
				              <span>ДЕКОРЫ</span>
				              <span class="arrow"></span>
                            </a>
				        </div>
				        <div id="DecorContainer"  runat="server" class="category-content">
				          <!-- Список категорий декоров -->
				        </div>
			        </article>

                      <article class="category-container">
			    	    <div class="category" onclick="toggleCategory('PromotionContainer', 4, event)">
                            <a id="PromotionLink" href="/Production?type=4">
			    		        <img class="category-img" src="/Images/promotion.png" alt="">
				              <span>РЕКЛАМНАЯ ПРОДУКЦИЯ</span>
				              <span class="arrow"></span>
                            </a>
				        </div>
				        <div id="PromotionContainer" runat="server" class="category-content">
				          <!-- Список категорий декоров -->
				        </div>
			        </article>

			        <article class="category-container">
			    	    <div class="category" onclick="toggleCategory('InteriorContainer', 5, event)">
                            <a id="InteriorLink" href="/Production?type=5">
			    		        <img class="category-img" src="/Images/interior.png" alt="">
				              <span>ИНТЕРЬЕРНЫЕ ДЕКОРЫ</span>
				              <span class="arrow"></span>
                            </a>
				        </div>
				        <div id="InteriorContainer" runat="server" class="category-content">
				          <!-- Список категорий декоров -->
				        </div>
			        </article>

                      <div id="ReadyContainer" runat="server" class=""></div>
			      </div>
			    </div>



			    <script>
                    function toggleCategory(containerId, link, event) {
                        event.preventDefault();
                        var container = document.getElementById(containerId);
                        var arrow = container.previousElementSibling.querySelector('.arrow');
                        var siblings = container.parentElement.parentElement.getElementsByClassName('category-content');

                        var currentPageType = parseInt(getParameterByName('type'));
                        
                        if (currentPageType !== link) {
                            window.location.href = "/Production?type=" + link;
                            //return;
                        }

                        container.classList.toggle('show');
                        arrow.classList.toggle('up');

                        for (var i = 0; i < siblings.length; i++) {
                            var sibling = siblings[i];
                            var siblingArrow = sibling.previousElementSibling.querySelector('.arrow');
                            if (sibling !== container) {
                                sibling.classList.remove('show');
                                siblingArrow.classList.remove('up');
                            }
                        }

                        //container.classList.toggle('show');
                        //arrow.classList.toggle('up');
                        localStorage.setItem('containerId', containerId);
                    }


                    // Функция для получения значения параметра из URL
                    function getParameterByName(name) {
                        name = name.replace(/[\[\]]/g, '\\$&');
                        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                            results = regex.exec(window.location.href);
                        if (!results) return null;
                        if (!results[2]) return '';
                        return decodeURIComponent(results[2].replace(/\+/g, ' '));
                    }

                    document.addEventListener('DOMContentLoaded', function () {
                        // Код для выполнения после полной загрузки DOM
                        var containerId = localStorage.getItem("containerId");

                        var container = document.getElementById(containerId);
                        var arrow = container.previousElementSibling.querySelector('.arrow');

                        container.classList.toggle('show');
                        arrow.classList.toggle('up');
                    });
                </script>	
		    </nav>













            <div style="display:inline-block; width:680px; text-align:left">
                <div id="ProductItemCont" runat="server">
                    <span id="ProductItemName" runat="server"></span>
                    <%--<div class="image-div">
                        <div id="ProductItem" runat="server" onclick="OpenImage(this)">
                            <div class="prod-item-back">
                                <div>
                                    <img id="ProductItemImage" runat="server" src="about:blank" ondragstart="return false;"/>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <div class="image-div">
                        <div id="FrontSliderCont" runat="server" class="im-sl-back-f noselect">
                            <div>
                                <div id="FrontImagesSliderCont" style="position:relative" runat="server">
                                    <div>
                                        <div class="nav" id="FrontSliderNavCont" runat="server"></div>
                                    </div>
                                    <div id="FrontImageName" class="image-name" style="visibility: hidden"></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--<div class="image-div">
                        <div id="FrontSliderCont" runat="server" class="im-sl-back-f noselect">
                            <div>
                                <div id="FrontImagesSliderCont" style="position:relative" runat="server" class="slider-container">
                                    <div>
                                        <div class="nav" id="FrontSliderNavCont" runat="server"></div>
                                    </div>
                                    <div id="FrontImageName" class="image-name"></div>
                                </div>
                            </div>
                        </div>
                    </div>--%>


                    <div class="prod-item-inf">
                        <div id="MaterialDiv" runat="server">
                            <span class="head">Материал:</span>
                            <span class="text" id="Material" runat="server"></span>
                        </div>
                        <div id="SizesDiv" runat="server" style="padding-top:30px;">
                            <span class="head">Размеры:</span>
                            <span id="Sizes" runat="server" class="text"></span>
                        </div>
                        <div id="DescriptionDiv" runat="server" style="padding-top:30px;">
                            <span class="head">Описание:</span>
                            <span id="Description" runat="server" class="text"></span>
                        </div>
                    </div>
                </div>






                <div class="prod-item-inf">
                    <div id="NotBasicFrontsDiv" runat="server" style="padding-top:30px; display: none;">
                        <span class="head" style="font-weight: bold">Фасады:</span>
                        <div id="NotBasicFronts" runat="server" class="pr-menu-cont"></div>
                    </div>

                    <div id="RelatedDecorsDiv" runat="server" style="padding-top:30px; display: none;">
                        <span class="head" style="font-weight: bold">Декоры:</span>
                        <div id="RelatedDecors" runat="server" class="pr-menu-cont"></div>
                    </div>
                </div>





                <div id="ProductMenu" runat="server" class="pr-menu-cont">
                    <span id="MainDescriptionDiv" runat="server" style="-webkit-text-size-adjust: none; display:none; font-size:15px; text-align:justify; margin-top:0px; margin-left:20px; margin-right:20px; margin-bottom:20px;">

                    </span>
                    <div id="SliderCont" runat="server" class="im-sl-back noselect">
                        <div>
                            <div id="ImagesSliderCont" style="position:relative" runat="server">
                                <div>
                                    <div class="nav" id="SliderNavCont" runat="server"></div>
                                </div>
                                <div id="ImageName" class="image-name"></div>
                            </div>
                        </div>
                    </div>
                    <span id="DescriptionText" runat="server" style="-webkit-text-size-adjust: none; font-size:15px; margin-top: 10px; margin-bottom:20px;"></span>
                </div>
            </div>
        </div>
    <%--</div>--%>

    <div>
         
    </div>


    <div class="modal fade" id="ImageDialogModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none">
        <div class="modal-dialog" runat="server" style="width:80%; height:80%; margin:80px auto;">
            <div class="modal-content" runat="server" style="width:100%; height:100%;">
                 <div style="background-color:rgb(250,250,250); position:absolute; float:left; left:0px; right:0px; top:0px; bottom:0px; border-style:solid; border-width:1px; border-color:rgb(160,160,160);
                        -webkit-box-shadow: 0px 0px 12px 3px rgba(0, 0, 0, .2);
                        box-shadow: 0px 0px 12px 3px rgba(0, 0, 0, .2); padding:10px; margin:0;">
                        <img id="ItemImage" src="/Images/Loader.GIF" style="max-width:100%; max-height:100%; position: absolute; margin: auto; top: 0; left: 0; right: 0; bottom: 0;" />             
                </div>              
            </div>
        </div>
    </div>

<script>
    var ci = 1;
    var ih;
    var SliderNames;
    var SliderUrls;

    $(document).ready(function () {
        ShowContent();

        if (document.getElementById('hSlidesCount').value != "") {

            if (parseInt(document.getElementById('hSlidesCount').value) > 1)
                ih = setInterval(Loop, 4000);

            document.getElementById("ImageName").innerHTML = SliderNames[0]
            if (document.getElementById('hDesc').value != "")
                document.getElementById("DescriptionText").innerHTML = document.getElementById('hDesc').value
            else {
                document.getElementById("DescriptionText").style.display = "none";
            }
        }
        else
            document.getElementById("DescriptionText").style.display = "none";
    })

    //function OpenImage(e)
    //{
    //    $('#ImageDialogModal').modal('show');

    //    document.getElementById('ItemImage').src = document.getElementById('URL').value + e.id;
    //}

    //function OpenSliderImage() {
    //    $('#ImageDialogModal').modal('show');

    //    document.getElementById('ItemImage').src = Catalog.URL + SliderUrls[ci - 1];
    //}
    function OpenImage(e) {
        $('#ImageDialogModal').modal('show');

        document.getElementById('ItemImage').src = document.getElementById('URL').value + e.id;
    }

    function OpenSliderImage() {
        $('#ImageDialogModal').modal('show');

        document.getElementById('ItemImage').src = /*'/Images/ClientsCatalogImages/' + */SliderUrls[ci - 1];
    }


    function OpenFrontSliderImage() {
        $('#ImageDialogModal').modal('show');

        //if(ci == 0)
        //    document.getElementById('ItemImage').src = '/Images/ClientsCatalogImages/' + SliderUrls[ci - 1];
        //else
        document.getElementById('ItemImage').src = SliderUrls[ci - 1];
    }

    function Loop()
    {
        var i = -1;

        if (ci == parseInt(document.getElementById("hSlidesCount").value))
            i = 1;
        else
            i = ci + 1;

        ShowImage(i);
    }

    function ShowImage(i) {
        if (i == 1)
        {
            document.getElementById('im' + i.toString()).style.opacity = 1;

            $(document.getElementById('im' + ci.toString())).fadeTo("slow", 0, function () {
                document.getElementById('sl' + i.toString()).children[0].className = "sl-p";
                document.getElementById('sl' + ci.toString()).children[0].className = "";
                ci = i;
            });
        }
        else
            $(document.getElementById('im' + i.toString())).fadeTo("slow", 1, function () {
                document.getElementById('im' + ci.toString()).style.opacity = 0;
                document.getElementById('sl' + i.toString()).children[0].className = "sl-p";
                document.getElementById('sl' + ci.toString()).children[0].className = "";
                ci = i;
            });

        document.getElementById("ImageName").innerHTML = SliderNames[i - 1]
    }

    function SelectImage(i)
    {
        clearInterval(ih);

        if (i != ci) {
            if (i < ci) {
                document.getElementById('im' + i.toString()).style.opacity = 1;

                $(document.getElementById('im' + ci.toString())).fadeTo("slow", 0, function () {
                    document.getElementById('sl' + i.toString()).children[0].className = "sl-p";
                    document.getElementById('sl' + ci.toString()).children[0].className = "";
                    ci = i;
                });
            }
            else
                $(document.getElementById('im' + i.toString())).fadeTo("slow", 1, function () {
                    document.getElementById('im' + ci.toString()).style.opacity = 0;
                    document.getElementById('sl' + i.toString()).children[0].className = "sl-p";
                    document.getElementById('sl' + ci.toString()).children[0].className = "";
                    ci = i;
                });
        }
        ih = setInterval(Loop, 4000);
    }

    function SetSliderNames(list) {
        SliderNames = Array();

        var s = "";

        for (var i = 0; i < list.length; i++) {
            if (list[i] == ';') {
                SliderNames.push(s);
                s = "";
                continue;
            }

            s += list[i];
        }
    }

    function SetSliderUrls(list) {
        SliderUrls = Array();

        var s = "";

        for (var i = 0; i < list.length; i++) {
            if (list[i] == ';') {
                SliderUrls.push(s);
                s = "";
                continue;
            }

            s += list[i];
        }
    }

</script>

</asp:Content>
