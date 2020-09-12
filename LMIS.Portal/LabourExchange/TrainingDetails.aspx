<%@ Page Title="<%$ Resources:CommonControls, F021 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="TrainingDetails.aspx.cs" Inherits="LMIS.Portal.LabourExchange.TrainingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/TrainingDetails.js" async="async"></script>

    <div class="col-md-9 blog" data-bind="visible: VmList().Id">
        <div class="blog-item">
            <div class="row">
                <div class="col-xs-12 col-sm-3 text-center">
                    <div class="entry-meta">
                        <span id="publish_date" style="font-weight: bolder" data-bind="text: lmis.format.dateToString(VmList().StartDate) + ' - ' + lmis.format.dateToString(VmList().EndDate)"></span>
                        <span><i class="fa fa-building-o"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().Extras.OrgName"></a>
                        </span>
                        <span data-bind="visible: VmList().Extras.OrgUrl"><i class="fa fa-globe"></i>
                            <a href="#" target="_blank" data-bind="attr: { href: 'http://' +VmList().Extras.OrgUrl }"><%=GetGlobalResourceObject("CommonControls", "X_Website")%></a>
                        </span>
                        <span data-bind="visible: VmList().City"><i class="fa fa-location-arrow"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().City + ', ' + VmList().Country"></a>
                        </span>
                        <span data-bind="visible: VmList().Duration"><i class="fa fa-laptop"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().Duration + ' <%=GetGlobalResourceObject("CommonControls", "X_Hours")%>    '"></a>
                        </span>
                        <span data-bind="visible: VmList().Seats"><i class="fa fa-users"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().Seats + ' <%=GetGlobalResourceObject("CommonControls", "X_Seats")%>    '"></a>
                        </span>
                        <span data-bind="visible: typeof VmList().Cost === 'number'"><i class="fa fa-money"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().Cost + ' <%=GetGlobalResourceObject("CommonControls", "X_EGP")%>    '"></a>
                        </span>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-9 blog-content">
                    <h2 data-bind="text: VmList().Title"></h2>
                    <div data-bind="visible: VmList().Description">
                        <h3><%=GetGlobalResourceObject("CommonControls", "F021_CourseDesc")%></h3>
                        <p data-bind="text: lmis.globalString.toLocal(VmList().Description, true)"></p>
                    </div>
                    <div data-bind="visible: VmList().FileName">
                        <h3><%=GetGlobalResourceObject("CommonControls", "F021_CourseOutline")%></h3>
                        <a href="#" target="_blank" data-bind="text: VmList().FileName, attr: { href: lmis.x.downloadURL + VmList().FileName }"></a>
                    </div>
                    <div data-bind="visible: VmList().Address">
                        <h3><%=GetGlobalResourceObject("CommonControls", "X_Address")%></h3>
                        <p data-bind="text: lmis.globalString.toLocal(VmList().Address, true)"></p>
                    </div>
                    <div data-bind="visible: VmList().Skills" class="grd">
                        <h3><%=GetGlobalResourceObject("CommonControls", "F021_Skills")%></h3>
                        <table style="border-spacing: 0; width: 100%;">
                            <thead>
                                <tr>
                                    <th><%=GetGlobalResourceObject("CommonControls", "X_Industry")%></th>
                                    <th><%=GetGlobalResourceObject("CommonControls", "X_Skill")%></th>
                                    <th><%=GetGlobalResourceObject("CommonControls", "X_SkillType")%></th>
                                    <th><%=GetGlobalResourceObject("CommonControls", "X_SkillLevel")%></th>
                                </tr>
                            </thead>
                            <tbody data-bind="foreach: VmList().Skills">
                                <tr>
                                    <td data-bind="text: Industry.desc"></td>
                                    <td data-bind="text: Skill.desc"></td>
                                    <td data-bind="text: lmis.format.nullableString(Type, 'desc')"></td>
                                    <td data-bind="text: Level.desc"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div data-bind="visible: VmList().Occurrence && VmList().Occurrence.length > 0" class="post-tags">
                        <strong><%=GetGlobalResourceObject("CommonControls", "F021_Occurrence")%>: </strong>
                        <span data-bind="text: VmList().Occurrence ? VmList().Occurrence.join(', ') : ''"></span>
                    </div>
                    <div data-bind="visible: CanApply">
                        <input id="btnSave" data-bind="click: Apply" value="<%=GetGlobalResourceObject("CommonControls", "X_Apply")%>" class="btn btn-primary" type="button" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-3">
        <div class="widget categories">
        </div>
    </div>

</asp:Content>