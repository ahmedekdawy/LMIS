<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SkillsInformation.aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.SkillsInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row form-divider col-sm-12">
        <div class="col-lg-4">
            <hr>
        </div>
        <div class="col-lg-4 form-divider-title">
            <%=GetGlobalResourceObject("CommonControls", "F009")%>
        </div>
        <div class="col-lg-4">
            <hr>
        </div>
    </div>
    <section id="user-registration22">
        <div class="container white-bg padding20">
            <div class="col-md-12">
                <div class="panel with-nav-tabs panel-default">
                    <div class="panel-heading">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab1default" data-toggle="tab">My Account</a></li>
                            <li><a href="#tab2default" data-toggle="tab"><%=GetGlobalResourceObject("CommonControls", "F009_CV")%></a></li>
                            <li><a href="#tab3default" data-toggle="tab"><%=GetGlobalResourceObject("CommonControls", "F009_JobSearch")%></a></li>
                            <!--<li><a href="#tab4default" data-toggle="tab">Recommendations</a></li>-->
                        </ul>
                    </div>
                    <div class="panel-body">
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="tab1default">
                                <div class="col-lg-12">
                                    <div class="progress-wrap">
                                        <h3>Total Points: 100
                                            <img src="../images/star-on.png" /></h3>
                                        <div class="progress">
                                            <div class="progress-bar  color1" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 85%">
                                                <span class="bar-width">85% Complete</span>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <!--Done-->
                                    <div class="stepwizard">
                                        <div class="stepwizard-row setup-panel">
                                            <div class="stepwizard-step">
                                                <a href="PersonalInformation.aspx?mode=e" type="button" class="btn btn-default btn-circle" data-bind="click: PersonalInfo"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>1 </a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_PersonalInfo")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="EducationalInformation.aspx#anchor" type="button" class="btn btn-default btn-circle" data-bind="click: EducationInfo"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%></span>2</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_Education")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="ExperienceInformation.aspx#anchor" type="button" class="btn btn-default btn-circle" data-bind="click: ExperienceInfo"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%></span>3</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_Experience")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="#step-4" type="button" class=" btn btn-primary btn-circle"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>4</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_Skills")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="#step-5" type="button" class="btn btn-default btn-circle" disabled="disabled"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>5</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_TrainingandCertifications")%></p>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Done-->
                                    <form role="form">
                                        <div class="row form-divider col-sm-12">
                                            <div class="col-lg-4">
                                                <hr>
                                            </div>
                                            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F015_CourseSkills")%> *</div>
                                            <div class="col-lg-4">
                                                <hr>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
                                            <div class="form-group">
                                                <label><%=GetGlobalResourceObject("CommonControls", "X_Industry")%> *</label>
                                                <select class="form-control always-white validationElement" data-bind="options: IndustryOptions, value: Industry, optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                                                </select>
                                            </div>
                                        </div>

                                        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
                                            <div class="form-group">
                                                <label><%=GetGlobalResourceObject("CommonControls", "X_Skills")%> *</label>
                                                <select id="Skills" class="form-control text-center validationElement" multiple="multiple" data-bind="multiselect: lmis.defaults.multiselectOptions, selectedOptions: SkillSelections, foreach: SkillOptions">
                                                    <optgroup data-bind="attr: { label: $data.desc }, foreach: $data.options" label="">
                                                        <option data-bind="value: $data.id, text: $data.desc"></option>
                                                    </optgroup>
                                                </select>
                                                <div class="input-group">
                                                    <input type="text" id="txtNewSkill" class="form-control" maxlength="50" data-bind="value: NewSkill" />
                                                    <span class="input-group-btn">
                                                        <button id="btnNewSkill" class="btn btn-default" data-bind="click: AddSkill">
                                                            <i class="fa fa-plus"></i><%=GetGlobalResourceObject("CommonControls", "X_Add")%>
                                                        </button>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label><%=GetGlobalResourceObject("CommonControls", "X_YOfExperience")%> *</label>
                                                <input id="txtYOfExperience" type="text" name="name" class="form-control validationElement" data-bind="value: YOfExperience" />
                                            </div>
                                        </div>

                                        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
                                            <div class="text-center">
                                                <input value="<%=GetGlobalResourceObject("CommonControls", "X_Add")%>" class="btn btn-choose-graph btn-sm btn-success" style="width: 100px;" type="button" data-bind="click: AddSkills" />
                                                <input value="<%=GetGlobalResourceObject("CommonControls", "X_Clear")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearSkills" />
                                            </div>
                                        </div>
                                        <div class="col-sm-10 col-sm-offset-1 grd">
                                            <p></p>
                                            <table style="border-spacing: 0; width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th><%=GetGlobalResourceObject("CommonControls", "X_Industry")%></th>
                                                        <th><%=GetGlobalResourceObject("CommonControls", "X_Skill")%></th>
                                                        <th><%=GetGlobalResourceObject("CommonControls", "X_SkillType")%></th>
                                                        <th><%=GetGlobalResourceObject("CommonControls", "X_SkillLevel")%></th>
                                                        <th><%=GetGlobalResourceObject("CommonControls", "X_YOfExperience")%></th>
                                                        <th data-bind="visible: $root.Mode() !== 'v'">...</th>
                                                    </tr>
                                                </thead>
                                                <tbody data-bind="foreach: Skills">
                                                    <tr>
                                                        <td data-bind="text: Industry.desc"></td>
                                                        <td data-bind="text: Skill.desc"></td>
                                                        <td data-bind="text: lmis.format.nullableString(Type, 'desc')"></td>
                                                        <td data-bind="text: Level.desc"></td>
                                                        <td data-bind="text: YOfExperience"></td>
                                                        <td data-bind="visible: $root.Mode() !== 'v'">
                                                            <label data-bind="click: $root.RemoveSkill, css: $root.Mode() === 'v' ? 'disabledActionLabel' : 'actionLabel'">
                                                                <%=GetGlobalResourceObject("CommonControls", "X_Remove")%>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tbody data-bind="visible: Skills().length < 1">
                                                    <tr>
                                                        <td colspan="5">
                                                            <%=GetGlobalResourceObject("MessagesResource", "X_SelectSkills")%>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="col-sm-12">

                                            <button class="btn btn-primary nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow">Next</button>
                                        </div>
                                    </form>
                                </div>
                            </div>


                            <!-- <div class="tab-pane fade" id="tab4default">Default 4</div>-->
                        </div>
                    </div>
                </div>
            </div>
            <div style="display: none;">
                <div id="RequiredFieldsstep1">
                    <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
                    <p><%=GetGlobalResourceObject("MessagesResource", "F009_RequiredFieldsstep2")%></p>
                </div>
            </div>
        </div>
    </section>
    <script src="../IndividualRegistration/Scripts/SkillsInformation.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="Scripts/js.js"></script>

    <script>
        $(document).ready(function () {


            var navListItems = $('div.setup-panel div a'),
    allWells = $('.setup-content');
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

            $('div.setup-panel div a.btn-primary').trigger('click');

        });</script>
</asp:Content>
