@section Scripts {
    <script>
        $(document).ready(function() {
            setTimeout(function () {
                $('.alert').fadeOut('slow');
            }, 5000); // 5 seconds
        });

        @await Html.RenderPartialAsync("_ValidationScriptPartial");
    </script>
}
