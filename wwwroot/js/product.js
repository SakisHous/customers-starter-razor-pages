function confirmDelete(productId) {
    if (confirm("Are you sure to delete the product?")) {
        window.location.href = "/Products/" + productId + "/Delete"
    }
}