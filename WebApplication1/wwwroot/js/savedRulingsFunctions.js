
function getSavedCases() {
    var cases = localStorage.getItem("caseNR");

    if (cases === null) {
        cases = [];
    }
    var numberSavedCases = 0;
    var arrCases = [];

    if (cases.length > 0) {
        arrCases = cases.split(",");
        numberSavedCases = arrCases.length;
    }

    $(".savedRulingsNumber").html(" (" + numberSavedCases + ")");

    return arrCases;
}

function setSavedCase(caseNumber) {
    var index = -1;
    var arrCases = getSavedCases();
    for (let i = 0; i < arrCases.length; i++) {
        if (arrCases[i] == caseNumber)
            index = i;
    }
    if (index === -1) {
        arrCases.push(caseNumber);
    } else {
        arrCases.splice(index, 1);
    }

    localStorage.setItem("caseNR", arrCases.join(","))


    if (localStorage.getItem("caseNR") == '') {
        localStorage.removeItem("caseNR");
    }
    console.log(arrCases)
}

function setSavedStars() {

    var savedCases = getSavedCases();

    $(".saved-caseNumber").each(function () {
        var isSaved = false;

        for (let i = 0; i < savedCases.length; i++) {

            if ($(this).text().trim() == savedCases[i]) {
                isSaved = true;
            }
        }
        if (isSaved) {
            $(this).closest(".row").find(".container-star .star").addClass("saved");
        }
        else {
            $(this).closest(".row").find(".container-star .star").removeClass("saved");
        }
    })
}