
        $(document).ready(function () {
            // Add Category
            $("#addCategoryForm").submit(function (e) {
                e.preventDefault();
                $.ajax({
                    url: "/Category/Create",
                    type: "POST",
                    data: $(this).serialize(),
                    success: function () {
                        location.reload();
                    },
                    error: function () {
                        alert("An error occurred while adding the category.");
                    }
                });
            });

        $(".edit-btn").click(function () {
            $("#editId").val($(this).data("id"));
        $("#editName").val($(this).data("name"));
        $("#editDescription").val($(this).data("description"));
        $("#editCategoryModal").modal("show");
            });

        // Submit Edit Category Form
        $("#editCategoryForm").submit(function (e) {
            e.preventDefault();
        $.ajax({
            url: "/Category/Edit",
        type: "POST",
        data: $(this).serialize(),
        success: function (response) {
            alert("Category updated successfully.");
        location.reload();
                    },
        error: function () {
            alert("An error occurred while updating the category.");
                    }
                });
            });

        // Delete Category
        $(".delete-btn").click(function () {
                if (confirm("Are you sure you want to delete this category?")) {
            $.post("/Category/Delete", { id: $(this).data("id") }, function () {
                alert("Category deleted successfully.");
                location.reload();
            }).fail(function () {
                alert("An error occurred while deleting the category.");
            });
                }
            });
        });
   