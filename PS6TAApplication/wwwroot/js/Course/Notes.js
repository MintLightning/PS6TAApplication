/**
 * Author:   Matt McFarlane
 * Date:      10/1/2022
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and McFarlane - This work may not be copied for use in Academic Coursework.
 *
 * I, Matt McFarlane, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  Any references used in the completion of the assignment are cited in my README file.
 *
 * File Contents
 *
 *    Passes data to the CoursesController so that notes can be updated
 */

//Sends data to the CoursesController updateNote function
function updateNote(id, note) {
    $.post(
        {
            url: "/Courses/updateNote",
            data:
            {
                id: id,
                note: note
            }
        })
        .done(function (data) {
            console.log("data: ", data)
        });
}