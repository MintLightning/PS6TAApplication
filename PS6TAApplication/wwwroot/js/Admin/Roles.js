/**
 * Authors:   Matt McFarlane, Zach Post
 * Date:      10/1/2022
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and McFarlane/Post - This work may not be copied for use in Academic Coursework.
 *
 * I, Matt McFarlane, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  Any references used in the completion of the assignment are cited in my README file.
 * I, Zach Post, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  Any references used in the completion of the assignment are cited in my README file.
 *
 * File Contents
 *
 *    Passes data to the AdminController so that roles can be edited
 */

//Sends data to the AdminControllers toggleRole function
function toggleRole(id, role) {
    $.post(
        {
            url: "/Admin/toggleRole",
            data:
            {
                id: id,
                role: role
            }
        })
        .done(function (data) {
            console.log("data: ", data)
        });
}