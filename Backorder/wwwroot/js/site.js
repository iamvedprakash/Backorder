$(document).ready(function () {
    $(".itemrow").click(function () {
        $.ajax({
            url: "https://localhost:7035/Home/getItemStatus",
            type: "GET",
            data: {
                item: $(this).attr("id")
            },
            success: function (response) {
                getitemstatusdata(response);
            },
            error: function () {
                console.log("Error while fetching data");
            }

        })
    })

    $("#staussubmit").click(function () {
        $.ajax({
            url: "https://localhost:7035/Home/updateItemStatus",
            type: "POST",
            data: {
                item: $('#item').val(),
                issue: $('#issue').val(),
                comment: $('#comment').val(),
                recoverydate: $('#recovery-date').val(),
                POC: $('#poc').prop('checked'),
            },
            success: function () {
                alert("Data Updated")
            },
            error: function () {
                alert("Error")
            }
        })
    })

    $("#exportdata").click(function () {
        $.ajax({
            url: "https://localhost:7035/Home/exportData",
            type : "GET",
            success: function (response) {
                
            },
            error: function () {
                
            }
        })
    })


    function getitemstatusdata(data) {
        
        var date;

        $("#item").val(data.item);
        $("#issue").val(data.issue);
        $("#comment").val(data.comment);

        if (data.recoveryDate != null) {
            date = new Date(data.recoveryDate);
            $("#recovery-date").val(date.toISOString().slice(0, 10));
        }
        else {
            $("#recovery-date").val('');
        }
        $('#poc').prop('checked', data.poc);
        $("#modify-by").val(data.modifiedBy);

        if (data.modifiedDate != null) {
            date = new Date(data.modifiedDate);
            $("#modify-date").val(date.toISOString().slice(0, 10));
        }
        else {
            $("#modify-date").val('');
        }
    }

    $(document).ready(function () {
        $('#uploadBtn').click(function () {
            const fileInput = $('#fileInput')[0].files[0];
            const formData = new FormData();
            formData.append('file', fileInput);

            $.ajax({
                url: 'https://localhost:7035/Home/UpdateBulkData',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert('File uploaded successfully!');
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                    alert('An error occurred while uploading the file.');
                }
            });
        });
    });


})



var modal = document.getElementById("myModal");

var btn = document.getElementById("myBtn");

var span = document.getElementsByClassName("close")[0];

btn.onclick = function () {
    modal.style.display = "block";
}

span.onclick = function () {
    modal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}