<%@ Page Title="<%$ Resources:CommonControls,F035_Title %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="NewsFeed.aspx.cs" Inherits="LMIS.Portal.FrontEnd.NewsFeed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!-- main slider carousel -->
    <div class="row">
        <div class="col-md-12" id="slider">
            
                <div class="col-md-12" id="carousel-bounding-box">
                    <div id="myCarousel" class="carousel slide">
                        <!-- main slider carousel items -->
                        <div class="carousel-inner" data-bind="foreach: newsList">
                            <div class="item" data-bind="attr: { 'data-slide-number': $index() }, css: { 'active': $index() == 0 }" >
                                <img  class="img-responsive" data-bind="attr: { src: NewsBannerPath }" onerror="if (this.src != '../images/noimage.jpg') this.src = '../images/noimage.jpg';" class="img-responsive">
                                <div class="carousel-caption">
                               <a class="btn-slide animation animated-item-4"  data-bind="attr: { href: newshref }" target="_blank"> <h3 data-bind="    html: NewsTitle"></h3></a>
                            </div>
                            </div>
                            
                          
                        </div>
                        <!-- main slider carousel nav controls --> 
                        <!--<a class="carousel-control left" href="#myCarousel" data-slide="prev">‹</a>

                        <a class="carousel-control right" href="#myCarousel" data-slide="next">›</a>-->
                         <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left"></span>
  </a>
  <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right"></span>
  </a>
                        
                    </div>
                </div>

        </div>
    </div>
    <!--/main slider carousel-->
    
<div class="col-md-12 hidden-sm hidden-xs" id="slider-thumbs">
        
            <!-- thumb navigation carousel items -->
          <ul class="list-inline" data-bind="foreach: newsList">
          <li> <a  data-bind="attr: { id: 'carousel-selector-'+$index() }"  class="selected">
            <img  data-bind="attr: { src:  NewsIconPath }" onerror="if (this.src != '../images/noimage.jpg') this.src = '../images/noimage.jpg';" class="img-responsive" class="img-responsive" style="height: 75px;width:75px">
          </a></li>
         
            </ul>
        
    </div>
    <script src="../FrontEnd/Scripts/NewsFeed.js"></script>
</asp:Content>
