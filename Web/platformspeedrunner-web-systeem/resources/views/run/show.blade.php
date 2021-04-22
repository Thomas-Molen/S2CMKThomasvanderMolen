@extends('layouts.app')
@section('title', 'Ticket')
@section('content')
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            @if($run->active === 0)
            <div class="alert alert-danger alert-dismissible">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h5><i class="icon fas fa-ban"></i>Attention!</h5>
                This run has been archived and is no longer visible on the leaderboards or personal run lists!
            </div>
            @else
            <div class="row">
                <div class="col-6">
                    <h1 class="m-0 text-dark">Run: {{ $run->custom_name }}</h1>
                </div>
                <div class="col-5">
                    @if((new \App\Helpers\AuthenticationHelper)->IsCurrentUser($run->user_id))
                    <form action="{{ route('run.destroy',$run->id) }}" method="POST">
                        <h5><a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                        @csrf
                        @method('DELETE')

                        <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this run?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
                    @endif
                        Created on: {{ $run->created_at }} (UTC)</h5>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-7 column2">
                <div class="row">
                    <div class="col-11 column">
                        <h5>Information:</h5>
                        {{ $run->information }}
                    </div>
                </div>
                <div class="row">
                    <div class="col-11 column">
                        <h5>Comments:</h5>
                        @foreach(\App\Http\Controllers\CommentController::GetCommentsByRunId($run->id) as $comment)
                            <div class="col-11 column3">
                                <i class="fas fa-user"></i><h7>{{ (new \App\Http\Controllers\UserController)->GetUsername($comment->user_id)}}</h7><br>
                                <div style="word-wrap: break-word">
                                    {{ $comment->content }}
                                </div>
                            </div>
                            <br>
                        @endforeach
                    </div>
                </div>
            </div>
            <div class="col-3 column2">
                <div class="row">
                    <div class="col-9 column">
                        <h5>Details</h5>
                        Runner: {{ \App\Http\Controllers\UserController::GetUsername($run->user_id) }}<br>
                        Time: {{ \App\Http\Controllers\LeaderboardController::FormatTime($run->duration) }} (m:s:ms)<br>
                        Age: x days ago
                    </div>
                </div>
                <br>
                <div class="row">
                    <div class="col-9 column">
                        <h5>External Links:</h5>
                        <i class="fas fa-link"></i><a href="http://www.google.com">google.com</a><br>
                        <i class="fas fa-link"></i><a href="http://www.youtube.com">youtube.com/speedrunner</a>
                    </div>
                </div>
                <br>
            </div>
        </div>
    </div>
    @endif
@endsection
<style>
    body {
        background-color: #f5f6f9;
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
