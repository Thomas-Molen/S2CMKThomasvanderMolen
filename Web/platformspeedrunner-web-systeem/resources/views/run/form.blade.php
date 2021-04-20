<div class="box box-info padding-1">
    <div class="box-body">
        <div class="form-group">
            {{ Form::label('custom_name') }}
            {{ Form::text('custom_name', $run->custom_name, ['class' => 'form-control' . ($errors->has('custom_name') ? ' is-invalid' : ''), 'placeholder' => 'Custom name']) }}
            {!! $errors->first('custom_name', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group class-info">
            {{ Form::label('information') }}
            {{ Form::text('information', $run->information, ['class' => 'form-control run-info' . ($errors->has('user_id') ? ' is-invalid' : ''), 'placeholder' => 'Extra information']) }}
            {!! $errors->first('information', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        @if ((new App\Http\Helpers\AuthenticationHelper)->IsAdmin())
        <div class="form-group">
            {{ Form::label('user_id') }}
            {{ Form::text('user_id', $run->user_id, ['class' => 'form-control' . ($errors->has('user_id') ? ' is-invalid' : ''), 'placeholder' => 'User id']) }}
            {!! $errors->first('user_id', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('duration') }}
            {{ Form::number('duration', $run->duration, ['class' => 'form-control' . ($errors->has('duration') ? ' is-invalid' : ''), 'placeholder' => 'Millisecond']) }}
            {!! $errors->first('duration', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        @endif
    </div>
    <div class="box-footer mt20">
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</div>
<style>
</style>
