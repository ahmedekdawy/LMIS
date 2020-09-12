function ViewModel() {

    var self = this;
    var query = (typeof lmis.queryString["q"] === "undefined") ? "" : decodeURI(lmis.queryString["q"]);
    var key = (typeof lmis.queryString["k"] === "undefined") ? 0 : parseInt(lmis.queryString["k"]);

    //Initialize VM
    self.Query = query;
    self.DataSet = ko.observableArray([]);
    self.DataSetTitle = ko.observable();
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();

    //VM Operations
    self.LoadRecord = function () {

        lmis.ajax("../BackEnd/Review.aspx/LoadRecord", { query: query, key: key, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d && data.d.data && data.d.res) {
                    self.DataSetTitle(data.d.res.RecTypeName);
                    self.Approval(data.d.data.Approval);
                    self.RejectReason(data.d.data.RejectReason);
                    Object.keys(data.d.res).forEach(function(k) {
                        if (typeof data.d.data[k] !== "undefined")
                            self.DataSet.push({
                                label: data.d.res[k],
                                text: data.d.data[k],
                                starRating: k === "Rating",
                                trilingual: data.d.data[k].hasOwnProperty("English")
                                    && data.d.data[k].hasOwnProperty("French")
                                    && data.d.data[k].hasOwnProperty("Arabic")
                            });
                    });
                    self.InitStarRatings();

                    //Admin Review?
                    if (window.vmReview) window.vmReview.init(query, key);
                }
            });

    }
    self.InitStarRatings = function() {
        $(".StarRating").rating({
            starCaptions: function (val) {
                if (val < 3) return val;
                else return "High";
            },
            starCaptionClasses: function (val) {
                if (val < 3) return "label label-danger";
                else return "label label-success";
            },
            showClear: false
        });
    }

    //Initialize UI
    if (!lmis.string.isNullOrWhiteSpace(query) && !isNaN(key) && key > 0) {
        self.LoadRecord();
    }

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})