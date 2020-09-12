<%@ Page Title="<%$ Resources:CommonControls,F035_Title %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="NewsItem.aspx.cs" Inherits="LMIS.Portal.FrontEnd.NewsItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- InstanceBeginEditable name="EditRegion1" -->
   <section id="internal-page-section">
  
     <div class="container white-bg">
          

            <div class="col-md-12 blog">
                    <div class="blog-item" data-bind="foreach: newsList">
                        
                            <div class="col-sm-12 text-center">
                                    <div class="entry-meta">
                                        <span id="publish_date" data-bind="text: NewsDate"></span>
                                      
                                    </div>
                                </div>
                      
                       <div class="col-sm-6" >
                                   <img class="img-responsive img-blog " data-bind="attr: { src: NewsBannerPath }"  alt="" width="600" height="200"  onerror="if (this.src != '../images/noimage.jpg') this.src = '../images/noimage.jpg';" class="img-responsive"/>
                       </div>
                            
                 
                            <div class="col-sm-6" >  
                            
                                <div class="col-xs-12 col-sm-10 blog-content">
                                    <h2 data-bind="html: NewsTitle"> </h2>
                                    <p data-bind="html: NewsDescription"></p>

                                </div>
                            </div>
                               <div class="col-sm-12" >
                                  
                                  <video width="500" height="240" controls><source data-bind="attr: { src: NewsVideoPath }"></video>
                                   <%--<iframe data-bind="attr: { src: NewsVideoPath }" height="560" width="450" allowfullscreen frameborder="0" autoplay="0" controls></iframe>--%>
                      
                        </div><!--/.blog-item-->
                        
                         
                        
                       


                        <!--/#contact-page-->
                    </div>


            <!--/.get-started-->

            

            

        </div>
     <!--/.container-->
   </section>
   <!-- InstanceEndEditable -->
    <script src="../FrontEnd/Scripts/NewsItem.js"></script>
</asp:Content>
