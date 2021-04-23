@extends('layouts.app')
@section('title', 'Ticket')
@section('content')
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            @if($run->active === 0)
            <div class="alert alert-danger alert-dismissible">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                <h5><i class="icon fas fa-ban"></i>Attention!</h5>
                This run has been archived and is no longer visible on the leaderboards or personal run lists!
            </div>
            @else
            <div class="row">
                <div class="col-7">

                    <h1 class="m-0 text-dark">Run: {{ $run->custom_name }}</h1>
                </div>
                <div class="col-4">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="{{ route('leaderboard') }}">Leaderboard</a></li>
                        <li class="breadcrumb-item active">{{ $run->custom_name }}</li>
                    </ol>
                    <h4 class="m-0 text-dark" style="float: right">Created on: {{ $run->created_at }} (UTC)</h4>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
{{--        Left Bar--}}
        <div class="row">
            <div class="col-7 left-column">
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">Information:</h5>
                        <blockquote class="quote-info">
                            <p style="white-space: pre-line; word-wrap: break-word; margin-top:-3%">
                                {{ $run->information }}
                            </p>
                        </blockquote>
                    </div>
                </div>
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">Comments:</h5>
                        @foreach((new \App\Http\Controllers\CommentController)->GetCommentsByRunId($run->id) as $comment)
                            <div class="col-11">
                                <blockquote>
                                    <i class="fas fa-user"></i><b>{{ (new \App\Http\Controllers\UserController)->GetUsername($comment->user_id)}}</b>
                                    <div style="word-wrap: break-word">
                                        {{ $comment->content }}
                                    </div>
                                    @if((new \App\Http\Helpers\AuthenticationHelper)->IsCurrentUser($comment->user_id))
                                        <form action="{{ route('comment.destroy',$comment->id) }}" method="POST" style="margin-bottom: -1%">
                                            <a class="btn btn-sm btn-secondary" href="{{ route('comment.edit',$comment->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                            @csrf
                                            @method('DELETE')

                                            <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this run?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                        </form>
                                    @endif
                                </blockquote>
                            </div>
                        @endforeach
                        <a class="btn btn-sm btn-success" href="{{ route('leaderboard_create_comment', $run->id) }}"><i class="fas fa-fw fa-comments"></i> Add Comment</a>
                    </div>
                </div>
            </div>
{{--            Right Bar--}}
            <div class="col-4">
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">Details</h5>
                        <br>
                        Runner: {{ (new \App\Http\Controllers\UserController)->GetUsername($run->user_id) }}<br>
                        Time: {{ (new \App\Http\Controllers\LeaderboardController)->FormatTime($run->duration) }} (m:s:ms)
                    </div>
                </div>
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">External Links:</h5>
                        @if((new \App\Http\Helpers\AuthenticationHelper)->IsCurrentUser($run->user_id))
                            <div style="margin-top: 1%; margin-bottom: -4%">
                                <a class="btn btn-sm btn-secondary" data-toggle="dropdown" aria-expanded="false" href="#"><i class="fa fa-fw fa-edit"></i>Edit Link</a>
                                <div class="dropdown-menu">
                                    @foreach((new \App\Http\Controllers\LinkController)->GetLinksByRunId($run->id) as $link)
                                        <a class="dropdown-item" href="{{ route('link.edit',$link->id) }}">
                                            {{ $link->name }}
                                        </a>
                                    @endforeach
                                </div>
                            </div>
                        @endif
                        <br>
                        @foreach((new \App\Http\Controllers\LinkController)->GetLinksByRunId($run->id) as $link)
                        <i class="fas fa-link"></i><a href="{{ $link->url }}" target="_blank" rel="noopener noreferrer">{{ $link->name }}</a><br>
                        @endforeach
                    </div>
                </div>
                <div class="row" style="margin-top: 1%">
                    <div class="col-9">
                        @if((new \App\Http\Helpers\AuthenticationHelper)->IsCurrentUser($run->user_id))
                            <form action="{{ route('run.destroy',$run->id) }}" method="POST">
                                <a class="btn btn-sm btn-success" href="{{ route('run_create_link', $run->id) }}"><i class="fas fa-link"></i> Add Link</a>
                                <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-edit"></i> Edit</a>

                                @csrf
                                @method('DELETE')

                                <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this run?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
                            </form>
                        @endif
                    </div>
                </div>
            </div>
        </div>
        <a class="btn btn-secondary" href="{{ (new \App\Http\Helpers\RoutingHelper)->PreviousRoute() }}">Back</a><br><br>
    </div>
    @endif
@endsection
<style>
    .left-column{
        margin-left: 1% !important;
    }

    .column{
        background-color: white;
        margin: 0.5%;
        padding: 0.5%;
    }

    .column2{
        background-color: grey;
        margin-left: 1%;
    }

    .column3{
        background-color: lightblue;
    }
</style>
