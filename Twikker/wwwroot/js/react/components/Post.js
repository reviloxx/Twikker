import React from 'react';
import Reaction from './Reaction';
import * as ajaxhandler from '../ajax-handler';

export default class Post extends React.Component {
    constructor(props) {
        super(props);
    }    

    componentDidMount() {
        $(document).ready(function () {
            $('.post').mouseenter(function () {
                $(this).children('.delete-button').fadeIn(100);
            });
            $('.post').mouseleave(function () {
                $(this).children('.delete-button').fadeOut(100);
            });
        });
    }

    handleDeleteClick(e) {
        e.preventDefault();
        if (confirm("Do you really want to delete this post?")) {
            var data = new FormData();
            data.append('postId', this.props.postId);
            ajaxhandler.ajaxRequest(data, 'posts/delete', this.handleResponse.bind(this));
        }
    }    

    handleResponse(response) {
        this.props.onDeletedPost();
    }

    render() {
        if (this.props.activeUserId === this.props.creatorId) {
            return (
                <div className="post">
                    <button className="delete-button btn-danger btn-sm" onClick={this.handleDeleteClick.bind(this)}>Delete</button>
                    <h3 className="creator">{this.props.creator}</h3>                        
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h5 className="post-text">{this.props.children}</h5>
                    <Reaction textId={this.props.textId} reactions={this.props.reactions} activeUserId={this.props.activeUserId}/>
                </div>
            );
        } else {
            return (
                <div className="post">
                    <h3 className="creator">{this.props.creator}</h3>
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h5 className="post-text">{this.props.children}</h5>
                    <Reaction textId={this.props.textId} reactions={this.props.reactions} activeUserId={this.props.activeUserId} />
                </div>
            );
        }        
    }
}
Post.displayName = 'Post';