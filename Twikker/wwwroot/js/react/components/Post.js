import React from 'react';

export default class Post extends React.Component {
    constructor(props) {
        super(props);
        this.state = {postId: -1}
    }

    handleDeleteClick(e) {
        e.preventDefault();
        console.log("Delete Post: " + this.props.postId);
        var data = new FormData();
        data.append('postId', this.props.postId)

        var xhr = new XMLHttpRequest();
        xhr.open('post', "posts/delete", true);
        xhr.onload = function () {
            this.props.onDeletedPost();
        }.bind(this);
        xhr.send(data);
    }

    

    render() {
        if (this.props.activeUserId == this.props.creatorId) {
            return (
                <div className="post">
                    <div className="post-content">
                        <button className="delete-button btn-danger btn-sm" onClick={this.handleDeleteClick.bind(this)}>Delete</button>
                        <h2 className="creator">{this.props.creator}</h2>                        
                        <h4 className="creation-date">{this.props.creationDate}</h4>
                        <h4 className="post-text">{this.props.children}</h4>  
                    </div>
                </div>
            );
        } else {
            return (
                <div className="post">
                    <div className="post-content">
                        <h2 className="creator">{this.props.creator}</h2>
                        <h4 className="creation-date">{this.props.creationDate}</h4>
                        <h4 className="post-text">{this.props.children}</h4>
                    </div>
                </div>
            );
        }        
    }
}