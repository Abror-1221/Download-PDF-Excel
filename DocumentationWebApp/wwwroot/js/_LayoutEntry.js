document.addEventListener('DOMContentLoaded', function () {
    document.body.addEventListener('click', function (e) {
        const target = e.target.closest('#signOutLink');
        if (target) {
            e.preventDefault();
            window.location.href = '/Home/Index'; // Ganti sesuai landing page kamu
        }
    });
});
