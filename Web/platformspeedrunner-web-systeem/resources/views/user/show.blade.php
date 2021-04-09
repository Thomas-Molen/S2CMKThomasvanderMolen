@extends('layouts.app')

@section('title')
    Show user: {{ $user->username }}
@endsection

@section('content')
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>User: <strong>{{ $user->username }}</strong></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="{{ route('user.index') }}">Users</a></li>
                        <li class="breadcrumb-item active">Show user</li>
                    </ol>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group">
                                <strong>Username:</strong>
                                {{ $user->username }}
                            </div>
                            <div class="form-group">
                                <strong>Unique Key:</strong>
                                {{ $user->unique_key }}
                            </div>
                            <div class="form-group">
                                <strong>Upvotes:</strong>
                                {{ $user->upvotes }}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ route('user.index') }}">Back</a>
            <a class="btn btn-info" href="{{ route('user.edit',$user->id) }}">Edit</a>
        </div>
    </section>
@endsection
