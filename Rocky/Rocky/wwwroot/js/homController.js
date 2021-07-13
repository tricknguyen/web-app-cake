var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('#heart').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Home/IncreaseLove",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (reponse) {
                    $("#heart").removeClass("far fa-heart");
                    $("#heart").addClass("fas fa-heart disable-click");
                }
            });
        });
    }
}
user.init();