<div class="col-md-12" style="padding-top: 40px; padding-bottom: 40px;" data-bind="validationOptions: kovOptions">
    <div class="col-md-2">
    </div>
    <div class="col-md-8">
        <p data-bind="visible: mode() !== 'v'">
            <strong><%=GetGlobalResourceObject("MessagesResource", "W003_SelectServices")%></strong><br/><br/>
        </p>
        <div class="col-md-12">
            <div class="form-group">
                <label style="cursor: pointer">
                    <input type="checkbox" data-bind="checked: ReceiveTraining" />
                    <span><%=GetGlobalResourceObject("MessagesResource", "W003_ReceiveTraining")%></span>
                </label>
            </div>
            <div class="form-group">
                <label style="cursor: pointer">
                    <input type="checkbox" data-bind="checked: OfferJobs" />
                    <span><%=GetGlobalResourceObject("MessagesResource", "W003_OfferJobs")%></span>
                </label>
            </div>
            <div class="form-group">
                <label style="cursor: pointer">
                    <input type="checkbox" data-bind="checked: OfferTraining" />
                    <span><%=GetGlobalResourceObject("MessagesResource", "W003_OfferTraining")%></span>
                </label>
            </div>
            <div class="form-group" data-bind="visible: OfferTraining()">
                <label>
                    <span id="lblItcRegNo"><%=GetGlobalResourceObject("CommonControls", "W003_ItcRegNo")%></span>
                    <input type="text" maxlength="20" class="form-control" data-bind="textInput: ItcRegNo">
                </label>
            </div>
        </div>
    </div>
    <div class="col-md-2">
    </div>
</div>