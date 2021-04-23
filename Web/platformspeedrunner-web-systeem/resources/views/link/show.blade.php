@extends('layouts.app')

@section('template_title')
    {{ $link->name ?? 'Show Link' }}
@endsection

@section('content')
    <section class="content container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="float-left">
                            <span class="card-title">Show Link</span>
                        </div>
                        <div class="float-right">
                            <a class="btn btn-primary" href="{{ route('link.index') }}"> Back</a>
                        </div>
                    </div>

                    <div class="card-body">

                        <div class="form-group">
                            <strong>Name:</strong>
                            {{ $link->name }}
                        </div>
                        <div class="form-group">
                            <strong>Url:</strong>
                            {{ $link->url }}
                        </div>
                        <div class="form-group">
                            <strong>Run Id:</strong>
                            {{ $link->run_id }}
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
@endsection
