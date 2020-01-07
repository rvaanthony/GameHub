function AjaxHelper() {
    
    AjaxCall = function (address, method, data) {
        return $.ajax({
            type: method,
            url: address,
            datatype: 'json',
            contentType: "application/json; charset=utf-8",
            cache: false,
            data: data ? JSON.stringify(data) : null,
        }).fail(function (jqXHR, textStatus, errorThrown) {
        });
    }
        nonAsyncCall = function (uri, method, data) {
            return $.ajax({
                type: method,
                async: false,
                url: uri,
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: data ? JSON.stringify(data) : null
            }).fail(function (jqXHR, textStatus, errorThrown) {
            });
        }

}
