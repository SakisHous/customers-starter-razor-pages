function confirmDelete(customerId) {
    if (confirm("Are you sure for delete?")) {
        window.location.href = "/Customers/" + customerId + "/Delete"
    }
}