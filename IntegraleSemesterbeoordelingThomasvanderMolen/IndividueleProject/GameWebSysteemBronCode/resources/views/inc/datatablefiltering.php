<script>
    $(document).ready(function() {
        const table = $('.SpeedRunnerTable').DataTable();

        // Add filter box row
        $('.SpeedRunnerTable thead tr').clone().appendTo('.SpeedRunnerTable thead');
        $('.SpeedRunnerTable thead tr:eq(1) th').each( function (i) {
            $(this).removeClass();
            let title = $(this).text();
            switch (title) {
                // Add the different filtering options to every column in the row
                case 'Actions':
                    $(this).html( '<a class="btn btn-sm btn-secondary" id="ClearFilterButton" style="margin-bottom:0.5%" href="javascript:void(0);"><i class="fas fa-fw fa-unlink"></i> Clear Filters</a>' )
                    document.getElementById("ClearFilterButton").addEventListener("click", function() {
                        $( '.FilterInput' ).each(function () {
                            this.value = "";
                        });

                        //Reset Filter
                        $('.SpeedRunnerTable thead tr:eq(1) th').each( function (i) {
                            this.value = "";
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        });
                    });
                    break;
                default:
                    if (title === '#') {
                        title = "ID";
                    }
                    $(this).html( '<input type="text" class="FilterInput form-control" placeholder="'+title+'">' );
                    break;
            }

            // Filtering
            $( 'input', this ).on('keyup change', function () {
                if ( table.column(i).search() !== this.value ) {
                    table
                        .column(i)
                        .search(this.value)
                        .draw();
                }
            });
        });
    });
</script>
