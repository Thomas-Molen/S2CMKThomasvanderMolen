@extends('layouts.app')

@section('template_title')
    {{ $link->name ?? 'Show Link' }}
@endsection

@section('content')
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Link On Run: <strong>{{ $link->run->custom_name }}</strong></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="{{ route('link.index') }}">Links</a></li>
                        <li class="breadcrumb-item active">Show comment</li>
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
                                <strong>ID:</strong>
                                {{ $link->id }}
                            </div>
                            <div class="form-group">
                                <strong>Showcase Name:</strong>
                                {{ $link->name }}
                            </div>
                            <div class="form-group">
                                <strong>URL:</strong>
                                {{ $link->url }}
                            </div>
                            <div class="form-group">
                                <strong>Run:</strong>
                                {{ $link->run->custom_name }}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ route('link.index') }}">Back</a>
            <a class="btn btn-info" href="{{ route('link.edit',$link->id) }}">Edit</a>
        </div>
    </section>
@endsection
