@extends('layouts.app')
@section('title', 'Ticket')
@section('content')
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark">{{ $run->custom_name }}</h1>
                </div><!-- /.col -->
                <div class="col-3">
                    @if((new \App\Http\Helpers\AuthenticationHelper)->IsCurrentUser($run->user_id))
                    <form action="{{ route('run.destroy',$run->id) }}" method="POST">
                        <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                        @csrf
                        @method('DELETE')
                        <button type="submit" class="btn btn-danger btn-sm"><i class="fa fa-fw fa-trash"></i> Delete</button>
                    @endif
                        <h5>Created on: {{ $run->created_at }} (UTC)</h5>
                    </form>
                </div>
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

    <div class="container">
        <div class="row">
            <div class="col-9">
                <div class="row">
                    <div class="col-11 column">
                        <h6>Information:</h6>
                        {{ $run->information }}
                    </div>
                </div>
                <div class="row">
                    <div class="col-11 column"> Comments<br>
                        -Thomas - HELLO<br>
                        -UNKOWN - EPIC!
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="row">
                    <div class="col column">
                        <h6>Details</h6>
                        Runner: {{ (new \App\Http\Controllers\UserController)->GetUsername($run->user_id) }} <br>
                        Time: {{ (new App\Http\Controllers\LeaderboardController)->FormatTime($run->duration) }} <br>
                        Age:  x days  <!-- Add an age function here to calculate the age of the run-->
                    </div>
                </div>
                <div class="row">
                    <div class="col column">
                        <h6>Ticket files:</h6>
                        <form action="">
                            <input type="file" id="myFile" name="filename">
                            <input class="btn-primary" value="Upload" type="submit">
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection
<style>
    body {
        background-color: #f5f6f9;
    }

    .column{
        background-color: white;
        margin: 0.5%;
    }
</style>
