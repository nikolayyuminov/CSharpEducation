window.closeModal = function (modalId) {
    const modalElement = document.getElementById(modalId);

    if (!modalElement) {
        return;
    }

    const modal = bootstrap.Modal.getInstance(modalElement);

    if (modal) {
        modal.hide();
    }
};