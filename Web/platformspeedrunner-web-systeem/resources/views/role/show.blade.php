@extends('layouts.app')

@section('template_title')
    {{ $role->name ?? 'Show Role' }}
@endsection

@section('content')
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Role: <strong>{{ $role->name }}</strong></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="{{ route('role.index') }}">Roles</a></li>
                        <li class="breadcrumb-item active">Show role</li>
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
                                {{ $role->id }}
                            </div>
                            <div class="form-group">
                                <strong>name:</strong>
                                {{ $role->name }}
                            </div>
                            <div class="form-group">
                                <strong>Description:</strong>
                                {{ $role->description }}
                            </div>
                            <div class="form-group">
                                <strong>Active:</strong>
                                {{ $role->active }}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ route('role.index') }}">Back</a>
            <a class="btn btn-info" href="{{ route('role.edit', $role->id) }}">Edit</a>
        </div>
    </section>
@endsection
