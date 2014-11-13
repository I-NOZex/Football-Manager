$(function () {
    $("input.datepicker").datepicker({
        format: "dd/mm/yyyy"
    });

    //$(".select2").select2({ });
    //$("#CUSTOMERID").select2({
    //    initSelection: function (element, callback) {
    //        var elementText = "@ViewBag.currentItemName";
    //        callback({ "name": elementText });
    //    },
    //    placeholder: "Select an Item",
    //    allowClear: true,
    //    style: "display: inline-block",
    //    multiple: true,
    //    minimumInputLength: 2, //you can specify a min. query length to return results
    //    ajax:{
    //        cache: false,
    //        dataType: "json",
    //        type: "GET",
    //        url: "/Championships/FetchTeams/",
    //    data: function (searchTerm) {
    //        return { query: searchTerm };
    //    },
    //    results: function (data) { 
    //        return {results: data}; 
    //    }
    //},
    //    formatResult: function(item) {
    //        return item.name;
    //    },
    //formatSelection: function(item){
    //    return item.name;
    //},
    //escapeMarkup: function (m) { return m; }
    //});
});