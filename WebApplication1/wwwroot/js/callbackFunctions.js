﻿var pages = 1;
var uniqeSearchString = "";

function searchCallback(data) {

    for (let i = 0; i < data.cases.length; i++) {

        data.cases[i].DATE_FORMATED = formatDate(data.cases[i].closeD_DATE);
        data.cases[i].principle = formatPK(data.cases[i].principle)
        //data.cases[i].searchWords = data.cases[i].searchWords;
        //data.cases[i].result = data.cases[i].result.trim();
        //console.log(data.cases[i].closeD_DATE);

    }
    //Company custom dropdown
    for (let i = 0; i < data.fCompanyName.length; i++) {
        data.fCompanyName[i].label = data.fCompanyName[i].name + " (" + data.fCompanyName[i].count + ")";
        data.fCompanyName[i].value = data.fCompanyName[i].name;
    }

    for (let i = 0; i < data.fCompanyName.length; i++) {
        document.querySelector('#companyNames').addOption(data.fCompanyName[i]);
    }

    //Type of incurance custom dropdown
    for (let i = 0; i < data.fInsuranceType.length; i++) {
        data.fInsuranceType[i].label = data.fInsuranceType[i].name + " (" + data.fInsuranceType[i].count + ")";
        data.fInsuranceType[i].value = data.fInsuranceType[i].name;
    }

    for (let i = 0; i < data.fInsuranceType.length; i++) {
        document.querySelector('#insuranceType').addOption(data.fInsuranceType[i]);
    }

    // This is where I populate containerData
    var template = $("#datalist").html();
    var rendered = Mustache.render(template, data);
    $("#containerData").html(rendered);

    //Shows how many rulings found  
    $(".totalRulingsSpan").html(data.total);

    setSavedStars();

}

function getRulingsByID_callback(data) {

    if (!data) {
        data = {
            cases: []
        };
    }

    for (let i = 0; i < data.cases.length; i++) {

        data.cases[i].DATE_FORMATED = formatDate(data.cases[i].closeD_DATE);
        data.cases[i].principle = formatPK(data.cases[i].principle)
        data.cases[i].searchWords = data.cases[i].searchWords;
        data.cases[i].result = data.cases[i].result.trim();
        console.log(data.cases[i].closeD_DATE);

    }

    // This is where I populate localSavedCases
    var template = $("#datalistsaved").html();
    var rendered = Mustache.render(template, data);
    $("#localSavedCases").html(rendered);

    setSavedStars();

}


function getDataObject() {
    var postData = {
        "words": $("#txtInputAutocomplete").val(),
        "orderby": $(".container-header-content").data("orderby"),
        "searchby": $(".container-header-content").data("searchby"),
        "pages": pages,
        "principal": $("#checkboxPK").is(":checked"),
        "complaintsUpheld": $("#checkboxComplainantUpheld").is(":checked"),
        "companyComplaintsUpheld": $("#checkboxCompanyUpheld").is(":checked"),
        "complaintsPartlyUpheld": $("#checkboxComplainantPartialUpheld").is(":checked"),
        "paragraph4": $("#checkboxComplaintDenied").is(":checked"),
        "insuranceType": $("#insuranceType").val(),
        "company": $("#companyNames").val(),
        "from": $("#fromDate_alt").val(),
        "to": $("#toDate_alt").val()


    }

    return postData;
}