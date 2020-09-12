<%@ Page Title="Harmonized Data Warehouse " Language="C#" AutoEventWireup="true" CodeBehind="Report_Entry.aspx.cs" Inherits="LMIS.Portal.Report_Entry"  MasterPageFile="~/MasterPages/FrontEnd.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
 
   <!-- InstanceBeginEditable name="EditRegion1" -->
    <section id="user-registration22" >
    <div class="container white-bg padding20">
    
    	
            <div class="panel with-nav-tabs panel-default">
                <div class="panel-heading" >
                    
                        <ul class="nav nav-tabs" runat="server" id="ulThemeType"  >
                           
                        </ul>
                </div>
                <div class="panel-body">
                    <br/>
                    <div class="tab-content" runat="server" id="divReports">
                     
                    </div>
                </div>
            </div>
    
    </div> 
    </section>
    
   
<script language="javascript" type="text/javascript">
   
     

   
    $(function () {
        $('[id^=carousel-selector-]').click(function () {
            var id_selector = $(this).attr("id");
            var id = id_selector.substr(id_selector.length - 1);
            id = parseInt(id);
            $('#myCarousel').carousel(id);
            $('[id^=carousel-selector-]').removeClass('selected');
            $(this).addClass('selected');
        });

        // when the carousel slides, auto update
        $('#myCarousel').on('slid', function (e) {
            var id = $('.item.active').data('slide-number');
            id = parseInt(id);
            $('[id^=carousel-selector-]').removeClass('selected');
            $('[id=carousel-selector-' + id + ']').addClass('selected');
        });
        $("#flexiselDemo3").flexisel({
            visibleItems: 5,
            animationSpeed: 1000,
            autoPlay: true,
            autoPlaySpeed: 3000,
            pauseOnHover: true,
            enableResponsiveBreakpoints: true,
            responsiveBreakpoints: {
                portrait: {
                    changePoint: 480,
                    visibleItems: 1
                },
                landscape: {
                    changePoint: 640,
                    visibleItems: 2
                },
                tablet: {
                    changePoint: 768,
                    visibleItems: 3
                }
            }
        });

    });
</script>


 
<script> $(document).ready(function () {

    var navListItems = $('div.setup-panel div a'),
        allWells = $('.setup-content'),
        allNextBtn = $('.nextBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-primary').addClass('btn-default');
            $item.addClass('btn-primary');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });
    $('#live-chat').click(function () { $('#live-chat-panel').show(300); });
    $('#live-chat-hide').click(function () { $('#live-chat-panel').hide(300); });
    $('#pie-chart').on({
        'click': function () {
            $('#my-graph').attr('src', '../images/tab03/pie-full.png');
        }
    });

    $('#bar-chart').on({
        'click': function () {
            $('#my-graph').attr('src', '../images/tab03/bar-chart.png');
        }
    });


    $('#line-chart').on({
        'click': function () {
            $('#my-graph').attr('src', '../images/tab03/line-full.png');
        }
    });


    $('#time-chart').on({
        'click': function () {
            $('#my-graph').attr('src', '../images/tab03/time-series.png');
        }
    });


    $('#pub-table').on({
        'click': function () {
            $('#my-pub-graph').attr('src', '../images/tab08/table-graphs.png');
        }
    });

    $('#pub-graph').on({
        'click': function () {
            $('#my-pub-graph').attr('src', '../images/tab08/graph-graphs.png');
        }
    });

    $('#pub-top10').on({
        'click': function () {
            $('#my-pub-graph').attr('src', '../images/tab08/top10-graphs.png');
        }
    });




    allNextBtn.click(function () {

        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid)
            nextStepWizard.removeAttr('disabled').trigger('click');
    });

    $('div.setup-panel div a.btn-primary').trigger('click');
});</script>
    </form>
   
</asp:Content>