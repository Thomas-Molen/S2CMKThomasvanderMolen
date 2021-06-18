@extends('layouts.app')

@section('template_title')
    Create Link
@endsection

@section('content')
    <section class="content container-fluid">
        <div class="row">
            <div class="col-md-12">

                @includeif('partials.errors')

                <div class="card card-default">
                    <div class="card-header">
                        <span class="card-title">Create Link</span>
                    </div>
                    <div class="card-body">
                        <form method="POST" action="{{ route('link.store') }}"  role="form" enctype="multipart/form-data">
                            @csrf

                            @include('link.form')

                        </form>
                    </div>
                </div>
                <a class="btn btn-secondary" href="javascript:history.go(-1)">Back</a>
            </div>
        </div>
    </section>
@endsection
