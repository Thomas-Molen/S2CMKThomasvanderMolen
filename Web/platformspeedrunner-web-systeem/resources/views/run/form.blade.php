<div class="box box-info padding-1">
    <div class="box-body">
        <div class="form-group">
            {{ Form::label('custom_name') }}
            {{ Form::text('custom_name', $run->custom_name, ['class' => 'form-control' . ($errors->has('custom_name') ? ' is-invalid' : ''), 'placeholder' => 'Custom name']) }}
            {!! $errors->first('custom_name', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group class-info">
            {{ Form::label('information (max: 5000 characters)') }}
            {{ Form::textarea('information', $run->information, ['class' => 'form-control run-info' . ($errors->has('user_id') ? ' is-invalid' : ''), 'placeholder' => 'Extra information']) }}
            {!! $errors->first('information', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        @if (auth()->user()->admin)
        <div class="form-group">
            {{ Form::label('active') }}
            {{ Form::text('active', $run->active, ['class' => 'form-control' . ($errors->has('active') ? ' is-invalid' : ''), 'placeholder' => 'active 1 or 0']) }}
            {!! $errors->first('active', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        @endif
    </div>
    <div class="box-footer mt20">
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</div>
<style>
</style>
