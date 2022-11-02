
$(function () {

$(".getMoreRulings").on("click", function () {
    pages = pages + 1;
    var rulings = $(".totalRulingsSpan").html()
    search(getDataObject(), false, searchCallback)

    if (pages > 9) {
        $(this).closest(".container-button-getRulings").addClass("ToManyRulings");
    }

    if (pages * 10 > rulings) {
        $(this).closest(".container-button-getRulings").addClass("noMoreRulings");
    }
});


    $(".container-header-content > div").on("click", function () {
        var searchBy = $(this).attr("searchby");

        var current = "";
        if ($(this).hasClass("asc")) {
            current = "asc";
        }
        else if ($(this).hasClass("desc")) {
            current = "desc";
        }

        $(this).closest(".container-header-content").find(">div").removeClass("asc desc");

        switch (current) {
            //case "asc":
            //    $(this).addClass("desc");
            //    break;
            case "desc":
                $(this).addClass("asc");
                break;
            default:
                $(this).addClass("desc");
                break;
        }
        var orderBy = $(this).hasClass("desc") ? "desc" : "asc";

        $(this).closest(".container-header-content").data("searchby", searchBy);
        $(this).closest(".container-header-content").data("orderby", orderBy);

        search(getDataObject(), false, searchCallback);

    });


    VirtualSelect.init({

        ele: '#companyNames',

        //options: myOptions,
        multiple: true,
        placeholder: 'Vælg',
        silentInitialValueSet: true,
    });
    VirtualSelect.init({
        ele: '#insuranceType',
        //options: myOptions,
        multiple: true,
        placeholder: 'Vælg',
        silentInitialValueSet: true,
    });


    $("#containerData").on("click", ".row div.arrow", function () {
        $(this).closest(".row").toggleClass("open");

    });

    $("#localSavedCases").on("click", ".row div.arrow", function () {
        $(this).closest(".row").toggleClass("open");

    });


    $(".container-outer.header").on("click", ".open-close-all-resumées span", function () {
        //TODO: Er det nødvendigt at sætte en klasse på begge id'er eller er der den smartere måde
        $("#containerData .row").addClass("open");
        $("#localSavedCases .row").addClass("open");
        $(this).addClass("open");

    });
    $(".container-outer.header").on("click", ".open-close-all-resumées span.open", function () {
        console.log("Click")
        //TODO: Er det nødvendigt at sætte en klasse på begge id'er eller er der den smartere måde(DETTE ER I FORBINDELSE MED GEMTE SAGER)
        $("#containerData .row").removeClass("open");
        $("#localSavedCases .row").removeClass("open");

        $(this).removeClass("open");
    });


    $("#containerData").on("click", ".row div.star", function () {
        $(this).toggleClass("saved");

        var itemToCheckFor = $(this).closest(".row").find(".saved-caseNumber").text();

        setSavedCase(itemToCheckFor)
        getSavedCases();
    });


    $("#localSavedCases").on("click", ".row div.star", function () {
        $(this).toggleClass("saved");

        var itemToCheckFor = $(this).closest(".row").find(".saved-caseNumber").text();
        setSavedCase(itemToCheckFor)
        getRulingsByID(getSavedCases(), getRulingsByID_callback);

        if (getSavedCases().length == 0) {
            console.log("One Time")
            $(".goToSavedRulings").trigger("click");
        }

    });


    $(".goToSavedRulings").on("click", function () {


        getRulingsByID(getSavedCases(), getRulingsByID_callback);
        $(".savedRulingsHidden").addClass("closed");
        $(".savedRulings").addClass("open");

        $(".goToSearchResult").removeClass("active");
        $(".goToSavedRulings").addClass("active")

        $(".totalRulings").addClass("active")

        $(".container-search-subscription").addClass("active")

        $(".container-button-getRulings").addClass("active")
        $(".container-text-under-getMoreRulingsButton").addClass("active")


        if (getSavedCases().length == 0) {
            $(".filler-when-empty").addClass("active")
        }

    });



    $(".goToSearchResult").on("click", function () {

        //$(".goToSearchResult").addClass("active");
        $(".goToSearchResult").addClass("active");
        $(".goToSavedRulings").removeClass("active")
        $(".starImage").removeClass("active")

        $(".savedRulingsHidden").removeClass("closed");
        $(".savedRulings").removeClass("open");

        $(".totalRulings").removeClass("active")

        $(".container-search-subscription").removeClass("active")

        $(".container-button-getRulings").removeClass("active")
        $(".container-text-under-getMoreRulingsButton").removeClass("active")

        $(".filler-when-empty").removeClass("active")

    });



    $(".removeAllSavedCases").on("click", function () {

        //$("#localSavedCases .row .container-star .star").removeClass("saved")
        //$("#containerData .row .container-star .star").removeClass("saved")

        localStorage.removeItem("caseNR")
        getRulingsByID(getSavedCases(), getRulingsByID_callback);

    });

    $(".modalSaveCase").on("click", function () {


        var itemToCheckFor = $(".modal-header").find("#modal-caseNumber").text();
        setSavedCase(itemToCheckFor)
        getRulingsByID(getSavedCases(), getRulingsByID_callback);
    });


    $("#containerData").on("click", ".container-outer.mobile ", function () {

        var da = $(this).closest('div').find(".dato-modal").text()
        var su = $(this).closest('div').find(".summary-modal").text()
        var re = $(this).closest('div').find(".result-modal").text()
        var cNu = $(this).closest('div').find(".caseNumber-modal").text()
        var sw = $(this).closest('div').find(".searchWords-modal").text()
        var cNa = $(this).closest('div').find(".companyName-modal").text()
        var it = $(this).closest('div').find(".insuranceType-modal").text()
        var pk = $(this).closest('div').find(".principle-modal").text()

        $("#modal-dato").html(da)
        $("#modal-summary").html(su)
        $("#modal-result").html(re)
        $("#modal-caseNumber").html(cNu)
        $("#modal-searchWords").html(sw)
        $("#modal-companyName").html(cNa)
        $("#modal-insuranceType").html(it)
        $("#modal-principle").html(pk)

    });

    $("#localSavedCases").on("click", ".container-outer.mobile ", function () {

        var da = $(this).closest('div').find(".dato-modal").text()
        var su = $(this).closest('div').find(".summary-modal").text()
        var re = $(this).closest('div').find(".result-modal").text()
        var cNu = $(this).closest('div').find(".caseNumber-modal").text()
        var sw = $(this).closest('div').find(".searchWords-modal").text()
        var cNa = $(this).closest('div').find(".companyName-modal").text()
        var it = $(this).closest('div').find(".insuranceType-modal").text()
        var pk = $(this).closest('div').find(".principle-modal").text()

        $("#modal-dato").html(da)
        $("#modal-summary").html(su)
        $("#modal-result").html(re)
        $("#modal-caseNumber").html(cNu)
        $("#modal-searchWords").html(sw)
        $("#modal-companyName").html(cNa)
        $("#modal-insuranceType").html(it)
        $("#modal-principle").html(pk)

    });

    $("#btngetsuggest").on("click", function () {
        suggest($("#txtinputsuggest").val(), suggestcallback);

    });



    $(".first-cbox-row > input").on("click", function () {
        $("#btnAutocomplete").trigger("click");

    });

    $(".second-cbox-row > input").on("click", function () {
        $("#btnAutocomplete").trigger("click");
    });

    $("#txtInputAutocomplete").on("keypress", function (event) {
        if (event.which == 13) {

            $("#btnAutocomplete").trigger("click");
            $("#txtInputAutocomplete").autocomplete("close");

        }
    });

    $("#btnAutocomplete").on("click", function () {
        search(getDataObject(), true, searchCallback);
    });

    $("#resetSearch").on("click", function () {

        document.getElementById("txtInputAutocomplete").value = "";
        $(".search-box-label input[type=checkbox]").prop('checked', false);

        if ($(".container-button-getRulings").hasClass("noMoreRulings")) {
            $(".container-button-getRulings").removeClass("noMoreRulings")
        }
        if ($(".container-button-getRulings").hasClass("ToManyRulings")) {
            $(".container-button-getRulings").removeClass("ToManyRulings")
        }

        $("#fromDate").val("");
        $("#fromDate_alt").val("");
        $("#toDate").val("");
        $("#toDate_alt").val("");

        pages = 1;
        search(getDataObject(), true, searchCallback);

    });

    $("#fromDate").datepicker({
        dateFormat: "dd-mm-yy",
        altFormat: "yy-mm-dd",
        altField: "#fromDate_alt",
    }).inputmask("99-99-9999").on("keypress", function (e) {
        if (e.which == 13) {
            $("#btnAutocomplete").trigger("click");
        }
    });

    $("#toDate").datepicker({
        dateFormat: "dd-mm-yy",
        altFormat: "yy-mm-dd",
        altField: "#toDate_alt"
    }).inputmask("99-99-9999").on("keypress", function (e) {
        if (e.which == 13) {
            $("#btnAutocomplete").trigger("click");
        }
    });


    $("#txtInputAutocomplete").autocomplete({
        source: function (request, response) {

            $.ajax({
                url: "/api/case/autocomplete",
                dataType: "json",
                headers: {
                    "accept": "application/json",
                    "Access-Control-Allow-Origin": "*"
                },
                data: {
                    "term": $("#txtInputAutocomplete").val()
                },
                success: function (data) {
                    response(data)

                }
            });

        },
        minLength: 3,
        select: function (event, ui) {
            event.preventDefault();
            console.log("Selected: ", ui.item.label);
            $("#txtInputAutocomplete").val(ui.item.label);

            search(getDataObject(), true, searchCallback);
        }
    }).autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>" + item.label + "</div>")
            .appendTo(ul);
    };

    //Her Kalder jeg search første gang.
    search(getDataObject(), true, searchCallback);

})