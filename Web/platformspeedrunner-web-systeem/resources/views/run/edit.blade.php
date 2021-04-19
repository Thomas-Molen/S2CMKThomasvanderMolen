@extends('layouts.app')

@section('title')
    Edit run: {{ $run->id }}
@endsection

@section('content')
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Edit run: <strong>{{ $run->custom_name }}</strong></h1>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <section class="content container-fluid">
        <div class="container-fluid">
            <div class="col-md-12">

                @includeif('partials.errors')

                <div class="card card-default">
                    <div class="card-body">
                        <form method="POST" action="{{ route('run.update', $run->id) }}"  role="form" enctype="multipart/form-data">
                            {{ method_field('PATCH') }}
                            @csrf

                            @include('run.form')

                        </form>
                    </div>
                </div>
            </div>
            @if( (new \App\Http\Controllers\AuthenticatorController)->IsAdmin())
            <a class="btn btn-secondary" href="{{ route('run.index') }}">Back</a>
                @else
                <a class="btn btn-secondary" href="{{ route('personal_runs') }}">Back</a>
            @endif
        </div>
    </section>
@endsection
