import React from 'react';

export default class Comment extends React.Component {
    //constructor(props) {
    //    super(props);
    //    this.state = { commentId: this }
    //}

    //handleDeleteClick(e) {
    //    e.preventDefault();
    //    console.log("Delete Post: " + this.props.postId);
    //    var data = new FormData();
    //    data.append('postId', this.props.postId)

    //    var xhr = new XMLHttpRequest();
    //    xhr.open('post', "posts/delete", true);
    //    xhr.onload = function () {
    //        this.props.onDeletedPost();
    //    }.bind(this);
    //    xhr.send(data);
    //}



    render() {
        if (this.props.activeUserId == this.props.creatorId) {
            return (
                <div className="comment">
                    <button className="delete-button btn-danger btn-sm" onClick={this.handleDeleteClick.bind(this)}>Delete</button>
                    <h4 className="creator">{this.props.creator}</h4>
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h6 className="comment-text">{this.props.children}</h6>
                </div>
            );
        } else {
            return (
                <div className="comment">
                    <h4 className="creator">{this.props.creator}</h4>
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h6 className="comment-text">{this.props.children}</h6>
                </div>
            );
        }
    }
}