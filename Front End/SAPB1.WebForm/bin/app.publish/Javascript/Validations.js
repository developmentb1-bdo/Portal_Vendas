// JScript File

// Validação de CPF e CNPJ
function valida_CPFCNPJ(oSrc, args) {
    if (args.Value.length == 11) {
        valida_CPF(oSrc, args);
    } else if (args.Value.length == 14) {
        valida_CNPJ(oSrc, args);
    } else {
        return args.IsValid = false;
    }
}
//Validação de IP
function validateIP(evt) {
    //Remember, this function will validate only Class C IP.
    //change to other IP Classes as you need
    ipaddr = ipaddr.replace(/\s/g, "") //remove spaces for checking
    var re = /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/; //regex. check for digits and in
    //all 4 quadrants of the IP
    if (re.test(ipaddr)) {
        //split into units with dots "."
        var parts = ipaddr.split(".");
        //if the first unit/quadrant of the IP is zero
        if (parseInt(parseFloat(parts[0])) == 0) {
            return false;
        }
        //if the fourth unit/quadrant of the IP is zero
        if (parseInt(parseFloat(parts[3])) == 0) {
            return false;
        }
        //if any part is greater than 255
        for (var i = 0; i < parts.length; i++) {
            if (parseInt(parseFloat(parts[i])) > 255) {
                return false;
            }
        }
        return true;
    } else {
        return false;
    }

}


//Validação de CPF
function valida_CPF(oSrc, args) {
    s = args.Value.replace(".", "").replace(".", "").replace("-", "");
    //args.isValid = (s >= 3);
    //document.write(oSrc.Value + ',' + args.Value);
    if (isNaN(s)) {
        return args.IsValid = false;
    }
    var i;
    var c = s.substr(0, 9);
    var dv = s.substr(9, 2);
    var d1 = 0;

    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (10 - i);
    }

    if (d1 == 0) {
        return args.IsValid = false;
    }

    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(0) != d1) {
        return args.IsValid = false;
    }

    d1 *= 2;
    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (11 - i);
    }

    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(1) != d1) {
        return args.IsValid = false;
    }

    return args.IsValid = true;
}

function valida_CPF2(args) {
    s = args.Value.replace(".", "").replace(".", "").replace("-", "");
    
    if (isNaN(s)) {
        return false;
    }

    var i;
    var c = s.substr(0, 9);
    var dv = s.substr(9, 2);
    var d1 = 0;

    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (10 - i);
    }

    if (d1 == 0) {
        return args.IsValid = false;
    }

    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(0) != d1) {
        return args.IsValid = false;
    }

    d1 *= 2;
    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (11 - i);
    }

    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(1) != d1) {
        return false;
    }

    return true;
}

//Validação de CNPJ
function valida_CNPJ(oSrc, args) {
    s = args.Value.replace(".", "").replace(".", "").replace("/", "").replace("-", "");
    if (isNaN(s)) {
        return args.IsValid = false;
    }
    var i;
    var c = s.substr(0, 12);
    var dv = s.substr(12, 2);
    var d1 = 0;
    for (i = 0; i < 12; i++) {
        d1 += c.charAt(11 - i) * (2 + (i % 8));
    }
    if (d1 == 0)
        return args.IsValid = false;
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(0) != d1) {
        return args.IsValid = false;
    }
    d1 *= 2;
    for (i = 0; i < 12; i++) {
        d1 += c.charAt(11 - i) * (2 + ((i + 1) % 8));
    }
    d1 = 11 - (d1 % 11);
    if (d1 > 9)
        d1 = 0;
    if (dv.charAt(1) != d1) {
        return args.IsValid = false;
    }
    return args.IsValid = true;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    else if (charCode === 8)
        return true;

    return true;
}

function isNumberAndVirgula(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode === 8 || charCode === 44)
        return true;
    else if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function formataData(OBJ, key) {
    var i, uBound;
    STR = new String("");

    //teclas delete, backspace, shift, nao disparam o evento
    if (key != 8 && key != 46 && key != 16 && !(key > 36 && key < 41)) {

        //retira caracteres nao numericos
        for (i = 0; i < OBJ.value.length; i++)
            if (OBJ.value.charAt(i) >= "0" && OBJ.value.charAt(i) <= "9")
            STR = STR + OBJ.value.charAt(i);
        OBJ.value = STR;

        if (OBJ.value.length < 8)
            uBound = OBJ.value.length;
        else
            uBound = 8;

        STR = "";
        for (i = 0; i < uBound; i++) {
            STR = STR + OBJ.value.charAt(i);
            if ((i == 1) || (i == 3))
                STR = STR + "/";
        }

        //if(STR.length > 6 && STR.length < 10){
        //	for (i=0; i < (10 - STR.length); i++){
        //		STR = STR + "0";
        //	}
        //}	

        OBJ.value = STR;
    }
}

function retornaKeyCode(evt) {
    var isNav, isIE;
    var theKey;

    if (parseInt(navigator.appVersion.charAt(0)) >= 4) {
        isNav = (navigator.appName == "Netscape") ? true : false;
        isIE = (navigator.appName.indexOf("Microsoft" != -1)) ? true : false;
    }

    if (isNav)
        theKey = evt.which;
    else if (isIE)
        theKey = window.event.keyCode;

    return theKey;
}

function validaData(source, args) {

    vlr = args.Value;
    dia = 0;
    mes = 0;
    ano = 0;
    //formato = formato.toUpperCase();
    formato = "DD/MM/AAAA"
    if (formato == "DD/MM/AAAA" || formato == "DD-MM-AAAA") {
        if (vlr.length != 10)
            return args.IsValid = false;

        dia = vlr.substr(0, 2);
        mes = vlr.substr(3, 2);
        ano = vlr.substr(6, 4);
        strSeparador = vlr.substr(2, 1)
    }
    else
        return args.IsValid = false;

    // Verifica barrinhas/tracinhos
    if (strSeparador == "/" || strSeparador == "-") {
        diafinal = "31";

        if (ano < "1900")
            return args.IsValid = false;

        if ((mes < "01") || (mes > "12"))
            return args.IsValid = false;

        if ((mes == "04") || (mes == "06") || (mes == "09") || (mes == "11"))
            diafinal = "30";

        if (mes == "02") {
            if ((ano % 4) == 0)
                diafinal = "29";
            else
                diafinal = "28";
        }

        if ((dia < "01") || (dia > diafinal))
            return args.IsValid = false;
    }
    else
        return args.IsValid = false;

    return args.IsValid = true;
}

function validaData02(vlr, formato) {
    dia = 0;
    mes = 0;
    ano = 0;
    formato = formato.toUpperCase();
    if (formato == "DD/MM/AAAA" || formato == "DD-MM-AAAA") {
        if (vlr.length != 10)
            return false;

        dia = vlr.substr(0, 2);
        mes = vlr.substr(3, 2);
        ano = vlr.substr(6, 4);
        strSeparador = vlr.substr(2, 1)
    }
    else
        return false;

    // Verifica barrinhas/tracinhos
    if (strSeparador == "/" || strSeparador == "-") {
        diafinal = "31";

        if (ano < "1900")
            return false;

        if ((mes < "01") || (mes > "12"))
            return false;

        if ((mes == "04") || (mes == "06") || (mes == "09") || (mes == "11"))
            diafinal = "30";

        if (mes == "02") {
            if ((ano % 4) == 0)
                diafinal = "29";
            else
                diafinal = "28";
        }

        if ((dia < "01") || (dia > diafinal))
            return false;
    }
    else
        return false;

    return true;
}

function Limpar(valor, validos) {
    // retira caracteres invalidos da string
    var result = "";
    var aux;
    for (var i = 0; i < valor.length; i++) {
        aux = validos.indexOf(valor.substring(i, i + 1));
        if (aux >= 0) {
            result += aux;
        }
    }
    return result;
}

//Formata número tipo moeda usando o evento onKeyDown

function Formata(campo, tammax, teclapres, decimal) {
    var tecla = teclapres.keyCode;
    vr = Limpar(campo.value, "0123456789");
    tam = vr.length;
    dec = decimal

    if (tam < tammax && tecla != 8) { tam = vr.length + 1; }

    if (tecla == 8)
    { tam = tam - 1; }

    if (tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105) {

        if (tam <= dec)
        { campo.value = vr; }

        if ((tam > dec) && (tam <= 3 + dec)) {
            campo.value = vr.substr(0, tam - dec) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 4 + dec) && (tam <= 6 + dec)) {
            campo.value = "";
            campo.value = vr.substr(0, tam - dec - 3) + "." + vr.substr(tam - dec - 3, 3) + "," + vr.substr(tam - dec, dec);
        }
        if ((tam >= 7 + dec) && (tam <= 9 + dec)) {
            campo.value = vr.substr(0, tam - dec - 6) + "." + vr.substr(tam - dec - 6, 3) + "." + vr.substr(tam - dec - 3, 3) + "," + vr.substr(tam - dec, dec);
        }
        if ((tam >= 10 + dec) && (tam <= 12 + dec)) {
            campo.value = vr.substr(0, tam - dec - 9) + "." + vr.substr(tam - dec - 9, 3) + "." + vr.substr(tam - dec - 6, 3) + "." + vr.substr(tam - dec - 3, 3) + "," + vr.substr(tam - dec, dec);
        }
        if ((tam >= 13 + dec) && (tam <= 15 + dec)) {
            campo.value = vr.substr(0, tam - dec - 12) + "." + vr.substr(tam - dec - 12, 3) + "." + vr.substr(tam - dec - 9, 3) + "." + vr.substr(tam - dec - 6, 3) + "." + vr.substr(tam - dec - 3, 3) + "," + vr.substr(tam - dec, dec);
        }
    }

}

function SelectAllCheckboxes(spanChk, nameChk) {

    // Added as ASPX uses SPAN for checkbox
    var oItem = spanChk.children;
    var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;

    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" && elm[i].id != theBox.id && elm[i].name == nameChk) {
        //elm[i].click();
        if (elm[i].checked != xState)
            elm[i].click();
        //elm[i].checked=xState;
    }
}

function SelectAllCheckboxes(spanChk) {

    var oItem = spanChk.children;
    var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;

    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
        if (elm[i].checked != xState)
            elm[i].click();
    }
}

function textCounter(campo, maxlimit) {
    if (campo.value.length > maxlimit) {
        campo.value = campo.value.substring(0, maxlimit);
        alert("Tamanho máximo do campo Observação atingido!");
    }
}

function limpar(campo) {

    var valorCampo = trim(campo.value);

    if (valorCampo == "0" || valorCampo == "0,0" || valorCampo == "0,00" || valorCampo == "0,000" || valorCampo == "0,0000" || valorCampo == "0,00000" || valorCampo == "0,000000" || valorCampo == "0,0000000") {
        campo.value = "";
    }
}

function trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function escrever(campo) {
    if (campo.value == "") { campo.value = campo.defaultValue; }
}

//adiciona mascara ao CPF
function MascaraCPF(cpf) {
    if (mascaraInteiro(cpf) == false) {
        event.returnValue = false;
    }
    return formataCampo(cpf, '000.000.000-00', event);
}
//mascara cnpj
function MascaraCNPj(cnpj) {
    if (mascaraInteiro(cnpj) == false) {
        event.returnValue = false;
    }
    return formataCampo(cnpj, '00.000.000/0000-00', event);
}
//mascara inteiro
function mascaraInteiro() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}
//formatar campo
function formataCampo(campo, Mascara, evento) {
    var boleanoMascara;

    var Digitato = evento.keyCode;
    exp = /\-|\.|\/|\(|\)| /g
    campoSoNumeros = campo.value.toString().replace(exp, "");

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length;;

    if (Digitato != 8) { // backspace 
        for (i = 0; i <= TamanhoMascara; i++) {
            boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
								|| (Mascara.charAt(i) == "/"))
            boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
								|| (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
        return true;
    } else {
        return true;
    }
}
//Placa de veiculos
function placa(v) {
    v = v.replace(/-/g, "");
    var pr = v.match(/-/);
    var v1 = v.substring(0, 3);
    var v2 = v.substring(3, 7);
    v1 = v1.replace(/\d|\W/g, "");
    v1 = v1.toUpperCase();
    v2 = v2.replace(/\D/g, "");
    v = v1;
    if (v2 != "")
        v += "-" + v2;

    return v;
}

//mascara de RG
function MascaraRG(rg) {
    if ((rg) == false) {
        event.returnValue = false;
    }
    return formataCampo(rg, '00.000.000-X', event);
}
// evento onBlur
function formataCPF(CPF) {
    with (CPF) {
        value = value.substr(0, 3) + '.' +
 				value.substr(3, 3) + '.' +
 				value.substr(6, 3) + '-' +
 				value.substr(9, 2);
    }
}

//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj) {
    var cnpj = ObjCnpj.value;
    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;

    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");
    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }
    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito)
        alert('CNPJ Invalido!');

}
//valida cpf


