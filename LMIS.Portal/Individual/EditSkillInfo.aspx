<%@ Page Title="<%$ Resources:CommonControls,F004_Skills %>" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="EditSkillInfo.aspx.cs" Inherits="LMIS.Portal.Individual.EditSkillInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <%--   <div class="row form-divider col-sm-12">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F015_CourseSkills")%> *</div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>--%>

        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Industry")%> *</label>
                <select class="always-white form-control validationElement" data-bind="options: IndustryOptions, value: Industry, optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
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
            <table style="border-spacing: 0; width: 100%;height: 100%; overflow: hidden">
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

            <button class="btn btn-primary nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow"><%=GetGlobalResourceObject("CommonControls", "X_Save")%></button>
        </div>
    </div>


    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Scripts/js.js"></script>
    <script src="../Individual/Scripts/edit-skill-vm.js" async="async"></script>
</asp:Content>
