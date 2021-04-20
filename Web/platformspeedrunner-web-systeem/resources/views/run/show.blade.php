@extends('layouts.app')

@section('title')
    Show run: {{ $run->id }}
@endsection

@section('content')
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Run: <strong>{{ $run->id }}</strong></h1>
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
                                <strong>ID:</strong>
                                {{ $run->id }}
                            </div>
                            <div class="form-group">
                                <strong>User ID:</strong>
                                {{ $run->user_id }}
                            </div>
                            <div class="form-group">
                                <strong>Duration:</strong>
                                {{ $run->duration }}
                            </div>
                            <div class="form-group">
                                <strong>Created at::</strong>
                                {{ $run->created_at }}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ (new \App\Http\Helpers\RoutingHelper)->PreviousRoute() }}">Back</a>
            <a class="btn btn-info" href="{{ route('run.edit',$run->id) }}">Edit</a>
        </div>
    </section>
@endsection
