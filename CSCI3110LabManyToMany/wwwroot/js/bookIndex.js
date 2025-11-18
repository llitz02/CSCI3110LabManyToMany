const bookIndex = (function () {
    let assignAuthorModal;

    const setupEventListeners = function () {
        const warningLinks = document.querySelectorAll('a.btn-warning');
        
        warningLinks.forEach(link => {
            link.addEventListener('click', function (event) {
                event.preventDefault();
                console.log(link.href);
                assignAuthorModal.show();
            });
        });

        const closeButton = document.querySelector('#assignAuthorModal .btn-close');
        if (closeButton) {
            closeButton.addEventListener('click', function () {
                assignAuthorModal.hide();
            });
        }
    };

    const main = function () {
        console.log('Book Index module loaded');
        const assignAuthorModalDOM = document.getElementById('assignAuthorModal');
        assignAuthorModal = new bootstrap.Modal(assignAuthorModalDOM);
        setupEventListeners();
    };

    return {
        init: main
    };
})();

bookIndex.init();
