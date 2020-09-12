function Event(item) {

    this.id= item.Id,
    this.title =item.Title;
    this.start = item.StartDate;
    this.end = item.EndDate;
    this.Type = item.Type;
    switch(item.Type ) {
        case 1: this.color = '#4aab73'; break; //green Event
        case 2: this.color = '#dfba24'; break;//yellow opportunity
        case 3: this.color = '#15385c'; break;//blue training
    };
};

function ViewModel() {

    var self = this;
  

 

    //VM Operations
    self.List = function () {

        lmis.ajax("../FrontEnd/Calendar.aspx/List", null, 0, "show,close",
            function (data) {
                var EventList = $.map(data.d, function (item) { return new Event(item) });
               
                $('#calendar').fullCalendar({
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,basicWeek,basicDay'
                    },
                    //defaultDate: '2016-06-12',
                    selectable: true,
                    selectHelper: true,
                    timeFormat: ' ',
                    eventClick: function (calEvent, jsEvent, view) {
                     
                        switch (calEvent.Type) {
                            case 1: window.open('../LabourExchange/EventDetails?m=v&k=' + calEvent.id, "_blank"); break; //green Event
                            case 2: window.open('../LabourExchange/OpportunityDetails?m=v&k=' + calEvent.id, "_blank"); break;//yellow opportunity
                            case 3: window.open('../LabourExchange/TrainingDetails?m=v&k=' + calEvent.id, "_blank"); break;//blue training
                        };
                       
                        $('#calendar').fullCalendar('unselect');
                    },

                    editable: true,
                    eventLimit: true, // allow "more" link when too many events
                    events: EventList
                });
            });

    }
 


    //Initialize UI
    self.List();

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})