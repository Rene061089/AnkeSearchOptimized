function suggest(input, callback) {

    $.ajax({
        url: "/api/case/suggest",
        type: "Post",
        data: {
            "fuzzy": true,
            "term": input
        },
        dataType: "json",
        headers: {
            "accept": "application/json",
            "Access-Control-Allow-Origin": "*"
        },
        beforeSend: function () {
            console.log("before");
        }
    })
        .done(function (data) {
            callback(data);

        })
        .fail(function () {

        })

}

function autocomplete(input, callback) {
    $.ajax({
        url: "/api/case/autocomplete",
        type: "POST",
        data: {
            "term": input
        },
        dataType: "json",
        headers: {
            "accept": "application/json",
            "Access-Control-Allow-Origin": "*"
        },
        beforeSend: function () {
            console.log("before");
        }
    })
        .done(function (data) {
            callback(data);


        })
        .fail(function () {

        })

}


function search(obj, isNewSearch, callback) {

    if (uniqeSearchString !== JSON.stringify(obj)) {
        uniqeSearchString = JSON.stringify(obj);

        if (isNewSearch) {
            pages = 1;
            obj.pages = pages;
        }

    }
    else {
        console.log("same search!!!")
        return false;
    }

    $.ajax({
        url: "/api/case/search",
        type: "POST",
        data: JSON.stringify(obj),

        dataType: "json",
        contentType: "application/json",
        headers: {
            "accept": "application/json",
            "Access-Control-Allow-Methods": "*"
        },
        beforeSend: function () {
            console.log("before");
        }
    })
        .done(function (data) {
            console.log(data);
            callback(data);


        })
        .fail(function () {

        })

}


function getRulingsByID(ids, callback) {

    if (Array.isArray(ids)) {
        if (ids.length > 0) {

            $.ajax({
                url: "/api/case/rulingsbyid",
                type: "POST",
                data: JSON.stringify(ids),

                dataType: "json",
                contentType: "application/json",
                headers: {
                    "accept": "application/json",
                    "Access-Control-Allow-Methods": "*"
                },
                beforeSend: function () {
                    console.log("before");
                }
            })
                .done(function (data) {
                    console.log(data);
                    callback(data);


                })
                .fail(function () {

                })

        }
        else {
            callback(null);
        }
    }

}



