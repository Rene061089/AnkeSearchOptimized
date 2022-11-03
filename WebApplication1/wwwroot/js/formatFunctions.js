//This is a small file where my fomat functions is, maby it will be bigger.

function formatPK(PK) {

    var no = "Nej"
    var yes = "Ja"

    if (PK == true) {
        return yes;
    } else {
        return no;
    }

}

function formatDate(d) {

    var nd = new Date(d);
    return ('0' + nd.getDate()).slice(-2) + '.' + ('0' + (nd.getMonth() + 1)).slice(-2) + '.' + nd.getFullYear();
}