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
            <div style="display: inline-block">
                <a class="btn btn-secondary" href="{{ $routingHelper->PreviousRoute() }}">Back</a>
            </div>
            @if($run->id !== null)
                <div style="display: inline-block">
                    <form action="{{ route('run.destroy',$run->id) }}" method="POST">

                        @csrf
                        @method('DELETE')

                        <button type="submit" class="btn btn-danger" onclick="return confirm('Are you sure you want to delete this run?');">Delete</button>
                    </form>
                </div>
            @endif
        </div>
    </section>
@endsection
