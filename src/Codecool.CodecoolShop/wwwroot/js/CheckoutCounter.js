var sum = document.getElementById("checkoutvalue").innerHTML;
var floatCartValue;
activeOnClickEvent();
onClickEventInput();

function activeOnClickEvent() {
    var cartValue = "";
    for (let i = 17; i < sum.length - 1; i++) {
        if (sum[i] == ",") {
            cartValue += ".";
        } else {
            cartValue += sum[i];
        }
    }
    floatCartValue = parseFloat(cartValue);
    console.log(floatCartValue);
}

function onClickEventInput() {
    var newSum = 0;
    var floatValue;
    var countQuantity;
    var cards = document.getElementsByClassName("product-quantity-container"); // product-description-container input-Quantity
    for (let i = 0; i < cards.length; i++) {
        console.dir(cards[i].childNodes);
        floatValue = transofrmToFloat(cards[i].childNodes[7].innerHTML);
        countQuantity = parseFloat(cards[i].childNodes[1].value);
        newSum += (floatValue * countQuantity);
    }
    console.log(newSum);
    document.getElementById("checkoutvalue").innerHTML = `Total cart value ${newSum}$`;
}


function transofrmToFloat(stringValue) {
    var returnFloat = "";
    for (let i = 0; i < stringValue.length - 1; i++) {
        if (stringValue[i] == ",") {
            returnFloat += ".";
        } else {
            returnFloat += stringValue[i];
        }
    }
    return parseFloat(returnFloat);

}