@extends('layouts.app')

@section('title')
    Create run
@endsection

@section('content')
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Create run</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="{{ route('run.index') }}">Runs</a></li>
                        <li class="breadcrumb-item active">Create run</li>
                    </ol>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">

                    @includeif('partials.errors')

                    <div class="card card-default">
                        <div class="card-body">
                            <form method="POST" action="{{ route('run.store') }}"  role="form" enctype="multipart/form-data">
                                @csrf

                                @include('run.form')

                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ route('run.index') }}">Back</a>
        </div>
    </section>
@endsection
